using LogLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RESTLib.Server.UnitTest
{
	[TestClass]
	public class RESTServerUnitTest
	{
		[TestMethod]
		public void ShouldCheckConstructorParameters()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new RESTServer(null, new RouteManager(new RouteParser()), "prefix"));
			Assert.ThrowsException<ArgumentNullException>(() => new RESTServer(NullLogger.Instance, null, "prefix"));
			Assert.ThrowsException<ArgumentNullException>(() => new RESTServer(NullLogger.Instance, new RouteManager(new RouteParser())));
			Assert.ThrowsException<ArgumentNullException>(() => new RESTServer(NullLogger.Instance, new RouteManager(new RouteParser()),null));
		}
	}
}
