using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Server.UnitTest.RouteHandlers
{
	public static class MethodInfos
	{
		public static MethodInfo GetBook;

		public static MethodInfo GetBooks;

		static MethodInfos()
		{
			GetBooks = RouteUtils.GetMethodInfo<BooksRouteHandler>("GetBooks");

			GetBook = RouteUtils.GetMethodInfo<BooksRouteHandler>("GetBook");

		}


	}
}
