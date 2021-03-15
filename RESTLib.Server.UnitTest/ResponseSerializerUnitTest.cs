using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RESTLib.Server.UnitTest
{
	[TestClass]
	public class ResponseSerializerUnitTest
	{
		[TestMethod]
		public void ShouldSerializeNullBody()
		{
			ResponseSerializer serializer;
			Response response;

			serializer = new ResponseSerializer();
			response=serializer.Serialize(null);
			Assert.AreEqual(ResponseCodes.NoContent, response.ResponseCode);
			Assert.AreEqual("", response.Body);

		}

	}
}
