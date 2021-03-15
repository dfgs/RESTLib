using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Client
{
    public class RESTClient
    {
        private HttpClient client;
        private IResponseDeserializer deserializer;

        public RESTClient(IResponseDeserializer Deserializer)
		{
            client = new HttpClient();
            this.deserializer = Deserializer;
        }
        public async Task<string> GetAsync(string URL)
		{
           
            HttpResponseMessage responseMessage;

            responseMessage= await client.GetAsync(URL);
            
            return await responseMessage.Content.ReadAsStringAsync();
		}
        public async Task<T> GetAsync<T>(string URL)
        {
            HttpResponseMessage responseMessage;
            Stream stream;

            responseMessage = await client.GetAsync(URL);
            stream=await responseMessage.Content.ReadAsStreamAsync();

            return deserializer.Deserialize<T>(stream);
        }

    }
}
