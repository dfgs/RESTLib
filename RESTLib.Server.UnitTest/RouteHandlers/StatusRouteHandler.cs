using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Server.UnitTest.RouteHandlers
{
	public class StatusRouteHandler:IRouteHandler
	{
		[Route(RESTMethods.GET, "root/API/Status")]
		public bool GetStatus()
		{
			return true;
		}

		



	}
}
