using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Server
{
	public class VariableRouteSegment:RouteSegment
	{
		public string Name
		{
			get;
		}
		public VariableRouteSegment(string Name)
		{
			this.Name = Name;
		}

	}
}
