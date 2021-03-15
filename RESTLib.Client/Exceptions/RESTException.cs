using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Client.Exceptions
{
	public class RESTException:Exception
	{
		public HttpStatusCode StatusCode
		{
			get;
			private set;
		}
		public RESTException(HttpStatusCode StatusCode):base($"HTTP returned {StatusCode} result")
		{
			this.StatusCode = StatusCode;
		}

	}
}
