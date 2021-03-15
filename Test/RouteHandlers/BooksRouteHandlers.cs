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
		public Book GetBook(int Id)
		{
			return new Book() { Id = Id,Year=2021, Author="Moliere", Title="Le bourgeois gentilhomme" };
		}
	}
}
