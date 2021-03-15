using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Client
{
	public class HttpConnector : IHttpConnector
	{
		private HttpClient client;
	
		public HttpConnector()
		{
			this.client = new HttpClient();
		}

		public async Task<HttpResponseMessage> GetResponseAsync(string URL)
		{
			return await client.GetAsync(URL);
		}
	}
}
