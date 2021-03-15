using RESTLib.Client.Exceptions;
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
        private IResponseDeserializer deserializer;
        private IHttpConnector httpConnector;

        public RESTClient(IHttpConnector HttpConnector, IResponseDeserializer Deserializer)
		{
            if (HttpConnector == null) throw new ArgumentNullException(nameof(HttpConnector));
            if (Deserializer == null) throw new ArgumentNullException(nameof(Deserializer));

            this.httpConnector = HttpConnector;
            this.deserializer = Deserializer;
        }
        public async Task<string> GetAsync(string URL)
		{
           
            HttpResponseMessage responseMessage;

            responseMessage= await httpConnector.GetResponseAsync(URL);
            if (responseMessage.StatusCode != System.Net.HttpStatusCode.OK) throw new RESTException(responseMessage.StatusCode);

            return await responseMessage.Content.ReadAsStringAsync();
		}
        public async Task<T> GetAsync<T>(string URL)
        {
            HttpResponseMessage responseMessage;
            Stream stream;

            responseMessage = await httpConnector.GetResponseAsync(URL);
            if (responseMessage.StatusCode != System.Net.HttpStatusCode.OK) throw new RESTException(responseMessage.StatusCode);

            stream = await responseMessage.Content.ReadAsStreamAsync();

            return deserializer.Deserialize<T>(stream);
        }

    }
}
