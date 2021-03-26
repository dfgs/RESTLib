using RESTLib.Server.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Server
{
	public class RouteManager : IRouteManager
	{
		private IRouteParser routeParser;
		private IRouteNode getNode;
		private IRouteNode putNode;
		private IRouteNode postNode;
		private IRouteNode deleteNode;
		private IResponseSerializer serializer;

		public RouteManager(IRouteParser RouteParser, IResponseSerializer Serializer)
		{
			if (RouteParser == null) throw new ArgumentNullException(nameof(RouteParser));
			if (Serializer == null) throw new ArgumentNullException(nameof(Serializer));
			this.routeParser = RouteParser;
			this.serializer = Serializer;
			getNode = new StaticRouteNode("root");
			putNode = new StaticRouteNode("root");
			postNode = new StaticRouteNode("root");
			deleteNode = new StaticRouteNode("root");
		}

		public void AddRouteHandler(IRouteHandler RouteHandler)
		{
			Type type;
			MethodInfo[] mis;
			RouteAttribute attribute;
			RouteSegment[] segments;

			if (RouteHandler == null) throw new ArgumentNullException(nameof(RouteHandler));

			type = RouteHandler.GetType();
			mis = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
			foreach (MethodInfo mi in mis)
			{
				attribute = mi.GetCustomAttribute<RouteAttribute>();
				if (attribute == null) continue;
				segments = routeParser.Parse(attribute.URL);
				CreateRoute(RouteHandler, mi,attribute.Method, segments);
			}

		}

		private IRouteNode GetRootNode(RESTMethods Method)
		{
			switch (Method)
			{
				case RESTMethods.GET: return getNode;
				case RESTMethods.PUT: return putNode;
				case RESTMethods.POST: return postNode;
				case RESTMethods.DELETE: return deleteNode;
				default: throw new InvalidParameterException($"REST Method {Method} not supported");
			}
		}

		public IRouteNode CreateRoute(IRouteHandler RouteHandler, MethodInfo MethodInfo,RESTMethods Method, params RouteSegment[] Segments)
		{
			IRouteNode currentNode;
			ParameterInfo[] pis;
			ParameterInfo pi;

			if (RouteHandler == null) throw new ArgumentNullException(nameof(RouteHandler));
			if (MethodInfo == null) throw new ArgumentNullException(nameof(MethodInfo));
			if (Segments == null) throw new ArgumentNullException(nameof(Segments));
			if (Segments.Length==0) throw new ArgumentNullException(nameof(Segments));

			pis = MethodInfo.GetParameters();

			if (Segments.OfType<VariableRouteSegment>().Count() != pis.Length) throw new InvalidRouteException(MethodInfo.Name);

			currentNode = GetRootNode(Method);

			foreach(RouteSegment segment in Segments)
			{
				switch(segment)
				{
					case StaticRouteSegment staticRouteSegment:
						currentNode = currentNode.CreateStaticNode(staticRouteSegment.Value);
						break;
					case VariableRouteSegment variableRouteSegment:
						currentNode = currentNode.CreateVariableNode(variableRouteSegment.Name);
						pi = pis.FirstOrDefault(item => item.Name == variableRouteSegment.Name);
						if (pi == null) throw new InvalidRouteException(MethodInfo.Name);
						break;
					default:
						throw new InvalidOperationException($"Invalid segment type {segment.GetType()}");
				}
			}
			
			if (currentNode.MethodInfo != null) throw new DuplicateRouteException(MethodInfo.Name);

			currentNode.RouteHandler = RouteHandler;
			currentNode.MethodInfo = MethodInfo;
			return currentNode;
		}
		public Route GetRoute(RESTMethods Method, string URL)
		{
			IRouteNode currentNode;
			VariableRouteNode variableRouteNode;
			StaticRouteNode staticRouteNode;

			Route route;

			string[] parts;

			if (string.IsNullOrEmpty(URL)) throw new ArgumentNullException(nameof(URL));

			route = new Route();
			parts = routeParser.Split(URL);

			currentNode = GetRootNode(Method);
			foreach (string value in parts)
			{
				staticRouteNode = currentNode.GetStaticNode(value);
				if (staticRouteNode == null)
				{
					variableRouteNode = currentNode.GetVariableNode();
					if (variableRouteNode == null) throw new RouteNotFoundException(URL);
					currentNode = variableRouteNode;
					route.Set(variableRouteNode.Name, value);
				}
				else currentNode = staticRouteNode;


			}

			if (currentNode.MethodInfo == null) throw new RouteNotFoundException(URL);
			route.RouteHandler = currentNode.RouteHandler;
			route.MethodInfo = currentNode.MethodInfo;
			return route;
		}


		public Response GetResponse(RESTMethods Method,string URL)
		{
			Route route;
			List<object> parameters;
			object result;

			if (string.IsNullOrEmpty(URL)) throw new ArgumentNullException(nameof(URL));

			route = GetRoute(Method, URL);
			parameters = new List<object>();


			foreach (ParameterInfo pi in route.MethodInfo.GetParameters())
			{
				try
				{
					parameters.Add(Convert.ChangeType(route.Get(pi.Name), pi.ParameterType));
				}
				catch
				{
					throw new InvalidParameterException(URL);
				}
				
			}

			try
			{
				result = route.MethodInfo.Invoke(route.RouteHandler, parameters.ToArray());
				if (result is Response response) return response;

				return serializer.Serialize(result);
			}
			catch
			{
				return Response.InternalError;
			}

						

	
		}


	}
}
