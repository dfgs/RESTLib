using RESTLib.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.RouteHandlers
{
	public class BooksRouteHandlers : IRouteHandler
	{
		[Route("/root/books/{Id}")]
		public string GetBook(int Id)
		{
			return $"book #{Id}";
		}
	}
}
