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

		public async Task<HttpResponseMessage> GetAsync(string URL)
		{
			return await client.GetAsync(URL);
		}
		public async Task<HttpResponseMessage> PostAsync(string URL)
		{
			return await client.PostAsync(URL, null);
		}
		public async Task<HttpResponseMessage> PutAsync(string URL)
		{
			return await client.PutAsync(URL, null);
		}

		public async Task<HttpResponseMessage> DeleteAsync(string URL)
		{
			return await client.DeleteAsync(URL);
		}


	}
}
