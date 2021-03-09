using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTLib.Server.UnitTest.RouteHandlers;
using System;
using System.Reflection;

namespace RESTLib.Server.UnitTest
{
	[TestClass]
	public class RouteUtilsUnitTest
	{
		[TestMethod]
		public void ShouldGetMethodInfo()
		{
			MethodInfo result;

			result = RouteUtils.GetMethodInfo<BooksRouteHandler>("GetBook");
			Assert.IsNotNull(result);
		}
		[TestMethod]
		public void ShouldNotGetMethodInfoWhenNameDoesntMatch()
		{
			MethodInfo result;

			result = RouteUtils.GetMethodInfo<BooksRouteHandler>("GetBook2");
			Assert.IsNull(result);
		}
		[TestMethod]
		public void ShouldNotGetMethodInfoWhenRouteAttributeNotSet()
		{
			MethodInfo result;

			result = RouteUtils.GetMethodInfo<BooksRouteHandler>("GetBook3");
			Assert.IsNull(result);
		}
	}
}
