using LogLib;
using RESTLib.Client;
using RESTLib.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Test.RouteHandlers;

namespace Test
{
	class Program
	{
		static async Task Main(string[] args)
		{
			RESTClient client;
			string result;
			Book book;

			RESTServer server;
			IRouteManager routeManager;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			routeManager.AddRouteHandler(new BooksRouteHandlers());
			server = new RESTServer(new ConsoleLogger(new DefaultLogFormatter()),routeManager, "http://localhost:8734/");

			server.Start();
			Thread.Sleep(1000);

			client = new RESTClient(new HttpConnector(), new ResponseDeserializer());

			/*Console.WriteLine("Trying to query URL");
			result = await client.GetAsync("http://localhost:8734/root/books/500");
			Console.WriteLine("Result:");
			Console.WriteLine(result);*/

			Console.WriteLine("Trying to query URL");
			book = await client.GetAsync<Book>("http://localhost:8734/root/books/500");
			Console.WriteLine("Result:");
			Console.WriteLine(book);

			Console.WriteLine("Trying to query URL");
			book = await client.GetAsync<Book>("http://localhost:8734/root/books?year=2020&author=stephenking");
			Console.WriteLine("Result:");
			Console.WriteLine(book);

			Console.WriteLine("Trying to query URL");
			try
			{
				book = await client.GetAsync<Book>("http://localhost:8734/root/book/500");
			}
			catch(Exception ex)
			{
				Console.WriteLine("Result:");
				Console.WriteLine(ex);
			}


			Console.ReadLine();

			server.Stop();
			Console.ReadLine();

		}
	}
}
