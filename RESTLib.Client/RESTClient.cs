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
    public class RESTClient:IRESTClient
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
        
        public async Task<T> GetAsync<T>(string URL)
        {
            HttpResponseMessage responseMessage;
            Stream stream;

            responseMessage = await httpConnector.GetAsync(URL);
            if (responseMessage.StatusCode != System.Net.HttpStatusCode.OK) throw new RESTException(responseMessage.StatusCode);

            stream = await responseMessage.Content.ReadAsStreamAsync();

            return deserializer.Deserialize<T>(stream);
        }
        public async Task<T> PostAsync<T>(string URL)
        {
            HttpResponseMessage responseMessage;
            Stream stream;

            
            responseMessage = await httpConnector.PostAsync(URL);
            if (responseMessage.StatusCode != System.Net.HttpStatusCode.OK) throw new RESTException(responseMessage.StatusCode);

            stream = await responseMessage.Content.ReadAsStreamAsync();

            return deserializer.Deserialize<T>(stream);
        }
        public async Task<T> PutAsync<T>(string URL)
        {
            HttpResponseMessage responseMessage;
            Stream stream;


            responseMessage = await httpConnector.PostAsync(URL);
            if (responseMessage.StatusCode != System.Net.HttpStatusCode.OK) throw new RESTException(responseMessage.StatusCode);

            stream = await responseMessage.Content.ReadAsStreamAsync();

            return deserializer.Deserialize<T>(stream);
        }
        public async Task<T> DeleteAsync<T>(string URL)
        {
            HttpResponseMessage responseMessage;
            Stream stream;


            responseMessage = await httpConnector.PostAsync(URL);
            if (responseMessage.StatusCode != System.Net.HttpStatusCode.OK) throw new RESTException(responseMessage.StatusCode);

            stream = await responseMessage.Content.ReadAsStreamAsync();

            return deserializer.Deserialize<T>(stream);
        }
    }
}
