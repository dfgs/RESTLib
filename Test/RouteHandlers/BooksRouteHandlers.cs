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
		[Route(RESTMethods.GET, "/root/books/{Id}")]
		public Book GetBook(int Id)
		{
			return new Book() { Id = Id,Year=2021, Author="Moliere", Title="Le bourgeois gentilhomme" };
		}
		[Route(RESTMethods.GET, "/root/books?year={Year}&author={Author}")]
		public Book GetBook(int Year,string Author)
		{
			return new Book() { Id = 999, Year = Year, Author = Author, Title = "Le bourgeois gentilhomme" };
		}

	}
}
