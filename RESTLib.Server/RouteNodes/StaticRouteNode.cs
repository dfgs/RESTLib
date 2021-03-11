using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Server
{
	public class StaticRouteNode:RouteNode
	{
		public string Value
		{
			get;
			private set;
		}

		public StaticRouteNode(string Value) :base()
		{
			if (string.IsNullOrEmpty(Value)) throw new ArgumentNullException(nameof(Value));
			this.Value = Value;
		}

	}
}
