using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Server.Exceptions
{
	public class InvalidURLException:RESTException
	{
		public string URL
		{
			get;
			private set;
		}

		public InvalidURLException(string URL)
		{
			this.URL = URL;
		}

	}
}
