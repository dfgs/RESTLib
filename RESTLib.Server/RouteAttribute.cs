using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Server
{
	[AttributeUsage(AttributeTargets.Method)]
	public class RouteAttribute:Attribute
	{
		public RESTMethods Method
		{
			get;
			set;
		}
		public string URL
		{
			get;
			set;
		}

		public RouteAttribute(RESTMethods Method, string URL)
		{
			this.Method = Method; this.URL = URL;
		}

	}
}
