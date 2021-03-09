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
		private IRouteNode node;

		public RouteManager(IRouteParser RouteParser)
		{
			if (RouteParser == null) throw new ArgumentNullException(nameof(RouteParser));
			this.routeParser = RouteParser;
			node = new StaticRouteNode("root");
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
				CreateRoute(RouteHandler, mi, segments);
			}

		}

		public IRouteNode CreateRoute(IRouteHandler RouteHandler, MethodInfo MethodInfo,params RouteSegment[] Segments)
		{
			IRouteNode currentNode;
			ParameterInfo[] pis;
			ParameterInfo pi;

			if (RouteHandler == null) throw new ArgumentNullException(nameof(RouteHandler));
			if (MethodInfo == null) throw new ArgumentNullException(nameof(MethodInfo));
			if (Segments == null) throw new ArgumentNullException(nameof(Segments));
			if (Segments.Length==0) throw new ArgumentNullException(nameof(Segments));

			pis = MethodInfo.GetParameters();

			if (Segments.OfType<VariableRouteSegment>().Count() != pis.Length) throw new InvalidOperationException($"Route variable count mismatch for method {MethodInfo.Name}");

			currentNode = node;
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
						if (pi == null) throw new InvalidOperationException($"Cannot map variable {variableRouteSegment.Name} to method info {MethodInfo.Name}");
						break;
					default:
						throw new InvalidOperationException($"Invalid segment type {segment.GetType()}");
				}
			}
			
			if (currentNode.MethodInfo != null) throw new InvalidOperationException("Route already exists");

			currentNode.RouteHandler = RouteHandler;
			currentNode.MethodInfo = MethodInfo;
			return currentNode;
		}
		public Route GetRoute(string URL)
		{
			IRouteNode currentNode;
			VariableRouteNode variableRouteNode;
			StaticRouteNode staticRouteNode;

			Route route;

			string[] parts;

			if (string.IsNullOrEmpty(URL)) throw new ArgumentNullException(nameof(URL));

			route = new Route();
			parts = routeParser.Split(URL);

			currentNode = node;
			foreach (string value in parts)
			{
				staticRouteNode = currentNode.GetStaticNode(value);
				if (staticRouteNode == null)
				{
					variableRouteNode = currentNode.GetVariableNode();
					if (variableRouteNode == null) throw new InvalidOperationException($"Route doesn't exist, segment {value}");
					currentNode = variableRouteNode;
					route.Set(variableRouteNode.Name, value);
				}
				else currentNode = staticRouteNode;


			}

			if (currentNode.MethodInfo == null) throw new InvalidOperationException($"Method info doesn't exist");
			route.RouteHandler = currentNode.RouteHandler;
			route.MethodInfo = currentNode.MethodInfo;
			return route;
		}


		public Response GetResponse(string URL)
		{
			Route route;
			List<object> parameters;
			Response reponse;
			object result;

			if (string.IsNullOrEmpty(URL)) throw new ArgumentNullException(nameof(URL));

			route = GetRoute(URL);
			parameters = new List<object>();


			foreach (ParameterInfo pi in route.MethodInfo.GetParameters())
			{
				try
				{
					parameters.Add(Convert.ChangeType(route.Get(pi.Name), pi.ParameterType));
				}
				catch
				{
					throw new InvalidOperationException($"Failed to convert parameter value {route.Get(pi.Name)} ({pi.Name})");
				}
				
			}

			try
			{
				result = route.MethodInfo.Invoke(route.RouteHandler, parameters.ToArray());
				return Response.OK(result.ToString());
			}
			catch
			{
				return Response.InternalError;
			}

						

	
		}


	}
}
