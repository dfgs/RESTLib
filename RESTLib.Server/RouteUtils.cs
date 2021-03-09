using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Server
{
	public static class RouteUtils
	{
		public static MethodInfo GetMethodInfo<T>(string Name)
		{
			Type type;
			MethodInfo[] mis;
			RouteAttribute attribute;

			type = typeof(T);
			mis = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
			foreach (MethodInfo mi in mis)
			{
				if (mi.Name != Name) continue;
				attribute = mi.GetCustomAttribute<RouteAttribute>();
				if (attribute == null) continue;
				return mi;
			}

			return null;
		}

	}
}
