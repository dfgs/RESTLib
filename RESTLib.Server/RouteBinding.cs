using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Server
{
	public class RouteBinding
	{

		public IRouteHandler RouteHandler
		{
			get;
			set;
		}
		public MethodInfo MethodInfo
		{
			get;
			set;
		}

		public RouteBinding()
		{
		}
		

	}
}
