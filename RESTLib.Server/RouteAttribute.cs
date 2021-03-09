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
		public string Value
		{
			get;
			set;
		}

		public RouteAttribute(string Value)
		{
			this.Value = Value;
		}

	}
}
