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
		Response GetResponse();

		IRouteNode CreateRoute(MethodInfo MethodInfo, params RouteSegment[] Segments);

	}
}
