using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Server
{
	public interface IRouteManager
	{
		Route GetRoute(RESTMethods Method, string URL);
		Response GetResponse(RESTMethods Method,string URL);

		IRouteNode CreateRoute(IRouteHandler RouteHandler, MethodInfo MethodInfo, RESTMethods Method, params RouteSegment[] Segments);

		void AddRouteHandler(IRouteHandler RouteHandler);
	}
}
