using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Client
{
	public interface IRESTClient
	{
		Task<T> GetAsync<T>(string URL);
		Task<T> PostAsync<T>(string URL);
		Task<T> PutAsync<T>(string URL);
		Task<T> DeleteAsync<T>(string URL);
	}
}
