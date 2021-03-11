using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Server
{
	public class VariableRouteNode:RouteNode
	{
		public string Name
		{
			get;
			private set;
		}

		public VariableRouteNode(string Name):base()
		{
			if (string.IsNullOrEmpty(Name)) throw new ArgumentNullException(nameof(Name));
			this.Name = Name;
		}

	}
}
