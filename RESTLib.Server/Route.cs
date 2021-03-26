using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Server
{
	public class Route
	{
		private Dictionary<string, object> variables;

		public RouteBinding Binding
		{
			get;
			set;
		}

		public Route()
		{
			variables = new Dictionary<string, object>();
		}
		public object Get(string Name)
		{
			object result;

			if (string.IsNullOrEmpty(Name)) throw new ArgumentNullException(nameof(Name));
			variables.TryGetValue(Name, out result);
			return result;
		}
		public void Set(string Name, object Value)
		{
			if (string.IsNullOrEmpty(Name)) throw new ArgumentNullException(nameof(Name));
			variables[Name] = Value;
		}


	}
}
