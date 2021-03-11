using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Server.Exceptions
{
	public class InvalidRouteException:RESTException
	{
		public string Method
		{
			get;
			private set;
		}

		public InvalidRouteException(string Method)
		{
			this.Method = Method;
		}

	}
}
