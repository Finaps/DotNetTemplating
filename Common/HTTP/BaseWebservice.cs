using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace MicroService.Common.HTTP
{
    /// <summary>
    /// The basic implementation of all Web Services which can be used as an abstraction (See RESTService)
    /// </summary>
    public class BaseWebService
    {
        static readonly HttpClient client = new HttpClient();
        string BaseServiceUrl { set; get; }
        protected enum RequestType { Delete, Get, Post, Put, Patch };

        /// <summary>
        /// Configures the HTTPClient with the specified BaseURI. 
        /// </summary>
        /// <param name="baseURI">Base URI.</param>
        protected void Init(string baseURI)
        {
            this.BaseServiceUrl = baseURI;
            client.BaseAddress = new Uri(BaseServiceUrl);
        }

        protected T Deserialize<T>(string json)
        {
            if (string.IsNullOrEmpty(json)) return default(T);

            var token = JToken.Parse(json);
            var obj = token.ToObject<T>();
            return obj;
        }

        protected Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            return SendAsync(RequestType.Delete, requestUri);
        }

        protected Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return SendAsync(RequestType.Get, requestUri);
        }

        protected Task<HttpResponseMessage> PatchAsync(string requestUri,
                                                      HttpContent content)
        {
            return SendAsync(RequestType.Patch, requestUri, content);
        }

        protected Task<HttpResponseMessage> PostAsync(string requestUri,
                                                      HttpContent content)
        {
            return SendAsync(RequestType.Post, requestUri, content);
        }

        protected Task<HttpResponseMessage> PostAsync(string requestUri)
        {
            return SendAsync(RequestType.Post, requestUri);
        }

        protected Task<HttpResponseMessage> PutAsync(string requestUri,
                                                     HttpContent content)
        {
            return SendAsync(RequestType.Put, requestUri, content);
        }

        protected Task<HttpResponseMessage> PutAsync(string requestUri)
        {
            return SendAsync(RequestType.Put, requestUri);
        }

        /// <summary>
        /// Clears the HTTPHeaders of the HttpClient
        /// </summary>
        public void clearHeaders()
        {
            client.DefaultRequestHeaders.Clear();
        }

        /// <summary>
        /// Adds a HTTPClient - does not overwrite 
        /// </summary>
        /// <param name="key">Header Key.</param>
        /// <param name="value">Value.</param>
        public void addHeader(string key, string value)
        {
            client.DefaultRequestHeaders.Add(key, value);
        }

        protected async Task<HttpResponseMessage> SendAsync(
            RequestType requestType, string requestUri,
            HttpContent content = null)
        {
            Task<HttpResponseMessage> httpTask;

            switch (requestType)
            {
                case RequestType.Delete:
                    httpTask = client.DeleteAsync(requestUri);
                    break;
                case RequestType.Get:
                    httpTask = client.GetAsync(requestUri);
                    break;
                case RequestType.Post:
                    httpTask = client.PostAsync(requestUri, content);
                    break;
                case RequestType.Put:
                    httpTask = client.PutAsync(requestUri, content);
                    break;
                case RequestType.Patch:
                    httpTask = client.PatchAsync(requestUri, content);
                    break;
                default:
                    throw new Exception("not valid request");
            }

            var response = await httpTask.ConfigureAwait(false);
            return response.EnsureSuccessStatusCode();
        }
    }
}
