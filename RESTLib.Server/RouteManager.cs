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
			node = new RouteNode();
		}

		public IRouteNode CreateRoute(MethodInfo MethodInfo,params RouteSegment[] Segments)
		{
			IRouteNode currentNode;
			if (MethodInfo == null) throw new ArgumentNullException(nameof(MethodInfo));
			if (Segments == null) throw new ArgumentNullException(nameof(Segments));
			if (Segments.Length==0) throw new ArgumentNullException(nameof(Segments));

			

			currentNode = node;
			foreach(RouteSegment segment in Segments)
			{
				switch(segment)
				{
					case StaticRouteSegment staticRouteSegment:
						currentNode = currentNode.GetStaticNode(staticRouteSegment.Value);
						break;
					case VariableRouteSegment staticRouteSegment:
						currentNode = currentNode.GetVariableNode();
						break;
					default:
						throw new InvalidOperationException($"Invalid segment type {segment.GetType()}");
				}

			}

			if (currentNode.MethodInfo != null) throw new InvalidOperationException("Route already exists");
			currentNode.MethodInfo = MethodInfo;
			return currentNode;
		}

		public Response GetResponse()
		{
			throw new NotImplementedException();
		}


	}
}
