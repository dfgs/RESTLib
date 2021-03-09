using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RESTLib.Server.UnitTest
{
	[TestClass]
	public class RouteUnitTest
	{
		[TestMethod]
		public void ShouldSetAndGetVariable()
		{
			Route route;

			route = new Route();
			route.Set("A", 1);
			Assert.AreEqual(1, route.Get("A"));
		}
		[TestMethod]
		public void ShouldNotSetWhenNameIsNull()
		{
			Route route;

			route = new Route();
			Assert.ThrowsException<ArgumentNullException>(() => route.Set(null, 1));
			Assert.ThrowsException<ArgumentNullException>(() => route.Set("", 1));
		}
		[TestMethod]
		public void ShouldNotGetWhenNameIsNull()
		{
			Route route;

			route = new Route();
			Assert.ThrowsException<ArgumentNullException>(() => route.Get(null));
			Assert.ThrowsException<ArgumentNullException>(() => route.Get(""));
		}

	}
}
