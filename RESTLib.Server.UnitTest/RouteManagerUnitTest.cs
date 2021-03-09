using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTLib.Server.UnitTest.RouteHandlers;
using System;
using System.Reflection;

namespace RESTLib.Server.UnitTest
{
	[TestClass]
	public class RouteManagerUnitTest
	{
		[TestMethod]
		public void ShouldCheckConstructorParameters()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new RouteManager(null));
		}
		[TestMethod]
		public void ShouldFailToCreateRouteIfParametersAreEmpty()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser());
			Assert.ThrowsException<ArgumentNullException>(() => routeManager.CreateRoute(null,new StaticRouteSegment("1")));
			Assert.ThrowsException<ArgumentNullException>(() => routeManager.CreateRoute(MethodInfos.MethodInfo1, null));
		}
		[TestMethod]
		public void ShouldCreateRoute()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser());
			routeManager.CreateRoute(MethodInfos.MethodInfo1, new StaticRouteSegment("root"), new StaticRouteSegment("API"), new StaticRouteSegment("Books"));
		}

		[TestMethod]
		public void ShouldCreateRouteWithVariableSegment()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser());
			routeManager.CreateRoute(MethodInfos.MethodInfo1, new StaticRouteSegment("root"), new StaticRouteSegment("API"), new StaticRouteSegment("Books"), new VariableRouteSegment("id"));
		}

		[TestMethod]
		public void ShouldNotCreateDuplicateRoute()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser());
			routeManager.CreateRoute(MethodInfos.MethodInfo1, new StaticRouteSegment("root"), new StaticRouteSegment("API"), new StaticRouteSegment("Books"));
			Assert.ThrowsException<InvalidOperationException>(()=> routeManager.CreateRoute(MethodInfos.MethodInfo1, new StaticRouteSegment("root"), new StaticRouteSegment("API"), new StaticRouteSegment("Books")));
		}

		[TestMethod]
		public void ShouldNoteCreateRouteWithVariableSegment()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser());
			routeManager.CreateRoute(MethodInfos.MethodInfo1, new StaticRouteSegment("root"), new StaticRouteSegment("API"), new StaticRouteSegment("Books"), new VariableRouteSegment("id"));
			Assert.ThrowsException<InvalidOperationException>(() => routeManager.CreateRoute(MethodInfos.MethodInfo1, new StaticRouteSegment("root"), new StaticRouteSegment("API"), new StaticRouteSegment("Books"), new VariableRouteSegment("id")));
		}

	}
}
