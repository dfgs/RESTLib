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
		Task<HttpResponseMessage> GetResponseAsync(string URL);
	}
}
