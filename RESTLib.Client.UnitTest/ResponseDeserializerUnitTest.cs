using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RESTLib.Client.UnitTest
{
	[TestClass]
	public class ResponseDeserializerUnitTest
	{
		[TestMethod]
		public void ShouldCheckConstructorParameters()
		{
			ResponseDeserializer deserializer;

			deserializer = new ResponseDeserializer();
			Assert.ThrowsException<ArgumentNullException>(() => deserializer.Deserialize<string>(null));
		}
	}
}
