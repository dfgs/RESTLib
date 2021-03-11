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
			if (string.IsNullOrEmpty(Name)) throw new ArgumentNullException(nameof(Name));
			this.Name = Name;
		}

	}
}
