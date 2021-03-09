using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Server.UnitTest.RouteHandlers
{
	public class TestRouteHandler1:IRouteHandler
	{
		[Route("root/API/Books/{id}")]
		public int GetNumber(int Id)
		{
			return Id;
		}

	}
}
