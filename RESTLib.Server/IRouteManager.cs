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
		Route GetRoute(string URL);
		Response GetResponse(string URL);

		IRouteNode CreateRoute(IRouteHandler RouteHandler, MethodInfo MethodInfo, params RouteSegment[] Segments);

		void AddRouteHandler(IRouteHandler RouteHandler);
	}
}
