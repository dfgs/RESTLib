﻿using LogLib;
using RESTLib.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
	class Program
	{
		static void Main(string[] args)
		{
			RESTServer server;
			IRouteManager routeManager;

			routeManager = new RouteManager(new RouteParser());
			server = new RESTServer(new ConsoleLogger(new DefaultLogFormatter()),routeManager, "http://localhost:8080/root/");

			server.Start();
			Console.ReadLine();

			server.Stop();
			Console.ReadLine();

		}
	}
}
