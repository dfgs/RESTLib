using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Server
{
	public class StaticRouteSegment:RouteSegment
	{
		public string Value
		{
			get;
		}
		public StaticRouteSegment(string Value)
		{
			if (string.IsNullOrEmpty(Value)) throw new ArgumentNullException(nameof(Value));
			this.Value = Value;
		}

	}
}
