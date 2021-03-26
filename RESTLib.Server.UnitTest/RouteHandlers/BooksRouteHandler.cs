using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Server.UnitTest.RouteHandlers
{
	public class BooksRouteHandler:IRouteHandler
	{
		[Route(RESTMethods.GET, "/root/API/Books")]
		public int GetBooks()
		{
			return 1;
		}
		[Route(RESTMethods.GET, "/root/API/books?year={Year}&author={Author}")]
		public string GetBooks(int Year, string Author)
		{
			return Author;
		}

		[Route(RESTMethods.GET, "/root/API/Books/{Id}")]
		public int GetBook(int Id)
		{
			return Id;
		}

		public int GetBook3(int Id)
		{
			return Id;
		}

		[Route(RESTMethods.GET, "/root/API/Authors/{Id}")]
		public int GetAuthor(int Id)
		{
			return Id;
		}

		[Route(RESTMethods.GET, "/root/API/Edition/{Id}/Year/{Year}")]
		public int GetEdition(int Id,int Year)
		{
			return Id;
		}

		[Route(RESTMethods.GET, "/root/API/GetNull")]
		public object GetNull()
		{
			return null;
		}
		[Route(RESTMethods.GET, "/root/API/GetCustomResponse")]
		public Response GetCustomResponse()
		{
			return new Response(ResponseCodes.Custom,"Custom", "text/html");
		}

	}
}
