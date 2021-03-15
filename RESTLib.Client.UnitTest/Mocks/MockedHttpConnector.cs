using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RESTLib.Client;

namespace RESTLib.Client.UnitTest.Mocks
{
	public class MockedHttpConnector : IHttpConnector
	{
		private System.Net.HttpStatusCode code;

		public MockedHttpConnector(System.Net.HttpStatusCode Code)
		{
			this.code = Code;
		}

		public async Task<HttpResponseMessage> GetResponseAsync(string URL)
		{
			HttpResponseMessage response;

			response = new HttpResponseMessage();
			response.StatusCode = code;

			return await Task.FromResult(response);
		}


	}
}
