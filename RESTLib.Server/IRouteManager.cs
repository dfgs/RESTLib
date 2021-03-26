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

		RouteBinding BindRoute(IRouteHandler RouteHandler, MethodInfo MethodInfo, RESTMethods Method, string Pattern);

		void AddRouteHandler(IRouteHandler RouteHandler);
	}
}
