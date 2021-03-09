using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Server.UnitTest.RouteHandlers
{
	public static class MethodInfos
	{
		public static MethodInfo MethodInfo1;

		static MethodInfos()
		{
			Type type;
			MethodInfo[] mis;
			RouteAttribute attribute;

			type = typeof(TestRouteHandler1);
			mis=type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
			foreach(MethodInfo mi in mis)
			{
				attribute=mi.GetCustomAttribute<RouteAttribute>();
				if (attribute == null) continue;
				MethodInfo1 = mi;
				break;
			}

		}

	}
}
