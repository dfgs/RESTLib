using LogLib;
using ModuleLib;
using RESTLib.Server.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RESTLib.Server
{
	public class RESTServer : ThreadModule,IRESTServer
	{
        private HttpListener listener;
        private IRouteManager routeManager;

        public RESTServer(ILogger Logger, IRouteManager RouteManager, params string[] Prefixes) : base(Logger)
		{
            if (RouteManager == null) throw new ArgumentNullException(nameof(RouteManager));

            this.routeManager = RouteManager;


            // for example "http://contoso.com:8080/index/".
            if (Prefixes == null || Prefixes.Length == 0) throw new ArgumentNullException(nameof(Prefixes));


            if (!HttpListener.IsSupported) throw new NotSupportedException("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");

            listener = new HttpListener();
            foreach (string s in Prefixes)
            {
                listener.Prefixes.Add(s);
            }
        }

		protected override void OnStopping()
		{
            LogEnter();
            Log(LogLevels.Information, "Stopping listener");
            Try(() => listener.Stop()).OrAlert("Failed to stop listener");
        }

        protected override void ThreadLoop()
		{
            HttpListenerContext context;
            HttpListenerRequest request;
            HttpListenerResponse response;
            Response routerManagerResponse;
            System.IO.Stream stream;
            RESTMethods method;

            LogEnter();


            Log(LogLevels.Information, "Starting listener");
            if (!Try(() => listener.Start()).OrAlert("Failed to start listener")) return;

            while (State==ModuleStates.Started)
            {
                Log(LogLevels.Information, "Waiting for incoming request");
                
                if (!Try(() => listener.GetContext()).OrAlert(out context, "Failed to get context")) continue;

                request = context.Request;
                response = context.Response;
                Log(LogLevels.Information, $"Received new request from {request.RemoteEndPoint}");

                switch(request.HttpMethod)
				{
                    case "GET":
                        method = RESTMethods.GET;
                        break;
                    case "PUT":
                        method = RESTMethods.PUT;
                        break;
                    case "POST":
                        method = RESTMethods.POST;
                        break;
                    case "DELETE":
                        method = RESTMethods.DELETE;
                        break;
                    default:
                        Log(LogLevels.Error, $"REST Method {request.HttpMethod} is not supported");
                        routerManagerResponse = Response.InternalError;
                        goto response;
                }


                Log(LogLevels.Information, $"Build response from router manager ({request.RawUrl})");
                try
				{
                     routerManagerResponse = routeManager.GetResponse(method,request.RawUrl);
                }
                catch (RouteNotFoundException)
                {
                    Log(LogLevels.Warning, $"Route was not found");
                    routerManagerResponse = Response.NotFound;
                }
                catch (InvalidParameterException)
                {
                    Log(LogLevels.Warning, $"Route contains invalid parameters");
                    routerManagerResponse = Response.BadRequest;
                }
                catch (Exception)
                {
                    Log(LogLevels.Error, $"Failed to build response");
                    routerManagerResponse = Response.InternalError;
				}

                response:
                Log(LogLevels.Information, $"Sending response to client");
                Try(() =>
                {
                    response.StatusCode = (int)routerManagerResponse.ResponseCode;
                    response.ContentEncoding = routerManagerResponse.Encoding;
                    byte[] buffer = routerManagerResponse.Encoding.GetBytes(routerManagerResponse.Body);
                    response.ContentLength64 = buffer.Length;
                    stream = response.OutputStream;
                    stream.Write(buffer, 0, buffer.Length);
                    stream.Close();
                }).OrAlert("Failed to send response");

            }
        }




	}
}
