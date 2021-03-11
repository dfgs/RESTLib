using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Server.Exceptions
{
	public class DuplicateRouteException:RESTException
	{
		public string Route
		{
			get;
			private set;
		}

		public DuplicateRouteException(string Route)
		{
			this.Route = Route;
		}

	}
}
