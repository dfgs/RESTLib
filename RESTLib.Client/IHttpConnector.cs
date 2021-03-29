using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Client
{
	public interface IHttpConnector
	{
		Task<HttpResponseMessage> GetAsync(string URL);
		Task<HttpResponseMessage> PostAsync(string URL);
		Task<HttpResponseMessage> PutAsync(string URL);
		Task<HttpResponseMessage> DeleteAsync(string URL);
	}
}
