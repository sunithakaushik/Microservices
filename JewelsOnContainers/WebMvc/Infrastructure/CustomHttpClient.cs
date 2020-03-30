using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebMvc.Infrastructure
{
    public class CustomHttpClient : IHttpClient
    {
        private readonly HttpClient _client;
        public CustomHttpClient()
        {
            _client = new HttpClient();
        }
        public async Task<string> GetStringAsync(string uri, 
            string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            // Step 6 in Module 16 http client making a request to Microservice
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            if(authorizationToken != null)
            {
                // Do this after token service
            }
            //step 7 in module 16 http client receives the response message from Microservice
            var response = await _client.SendAsync(requestMessage);
            // getting the response as a string and interested in only the content in response mesg.
            return await response.Content.ReadAsStringAsync();
        }

        public Task<HttpResponseMessage> PostAsync<T>(string uri, T item, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> PutAsync<T>(string uri, T item, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            throw new NotImplementedException();
        }
        public Task<HttpResponseMessage> DeleteAsync(string uri, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            throw new NotImplementedException();
        }
    }
}
