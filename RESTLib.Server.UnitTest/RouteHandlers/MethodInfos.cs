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

		static MethodInfos()
		{
			GetBook = RouteUtils.GetMethodInfo<BooksRouteHandler>("GetBook");

		}

		
	}
}
