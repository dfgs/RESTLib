using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RESTLib.Server.UnitTest
{
	[TestClass]
	public class ResponseUnitTest
	{
		[TestMethod]
		public void ShouldCheckConstructorParameters()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new Response(ResponseCodes.OK,null));
		}
	}
}
