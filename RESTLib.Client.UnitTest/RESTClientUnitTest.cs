using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTLib.Client.Exceptions;
using RESTLib.Client.UnitTest.Mocks;
using System;
using System.Threading.Tasks;

namespace RESTLib.Client.UnitTest
{
	[TestClass]
	public class RESTClientUnitTest
	{
		[TestMethod]
		public void ShouldCheckConstructorParameters()
		{

			Assert.ThrowsException<ArgumentNullException>(() => new RESTClient(null, new ResponseDeserializer())); ;
			Assert.ThrowsException<ArgumentNullException>(() => new RESTClient(new HttpConnector(), null)); ;
		}
		[TestMethod]
		public async Task ShouldThrowExceptionIfStatusCodeIsNotOK()
		{
			RESTClient client;

			client = new RESTClient(new MockedHttpConnector(System.Net.HttpStatusCode.NotFound), new ResponseDeserializer());
			
			await Assert.ThrowsExceptionAsync<RESTException>(() => client.GetAsync("root/test"));
			await Assert.ThrowsExceptionAsync<RESTException>(() => client.GetAsync<string>("root/test"));
		}


	}
}
