using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using communication.Interfaces;

namespace communication.WebService
{
  public class RestClient : BaseWebService, IRestClient
  {
    public async Task<T> DeleteAsync<T>(string requestUri)
    {
      var response = await DeleteAsync(requestUri);
      string json = await ReadResponse(response);
      return Deserialize<T>(json);
    }

    public async Task<T> GetAsync<T>(string requestUri)
    {
      var response = await GetAsync(requestUri);
      string json = await ReadResponse(response);
      return Deserialize<T>(json);
    }

    public async Task<T> PatchAsync<T, K>(string requestUri, K Object)
    {
      var request = PrepareRequest<K>(Object);
      var response = await PatchAsync(requestUri, request);
      var json = await ReadResponse(response);
      return Deserialize<T>(json);
    }

    public async Task<T> PostAsync<T, K>(string requestUri, K Object)
    {
      var request = PrepareRequest<K>(Object);
      var response = await PostAsync(requestUri, request);
      var json = await ReadResponse(response);
      return Deserialize<T>(json);
    }

    public async Task<T> PostAsync<T>(string requestUri)
    {
      var response = await PostAsync(requestUri);
      string json = await ReadResponse(response);
      return Deserialize<T>(json);
    }

    public async Task<T> PutAsync<T, K>(string requestUri, K Object)
    {
      var request = PrepareRequest<K>(Object);
      var response = await PutAsync(requestUri, request);
      var json = await ReadResponse(response);
      return Deserialize<T>(json);
    }

    public async Task<T> PutAsync<T>(string requestUri)
    {
      var response = await PutAsync(requestUri);
      string json = await ReadResponse(response);
      return Deserialize<T>(json);
    }

    private async Task<string> ReadResponse(HttpResponseMessage message)
    {
      return await message.Content.ReadAsStringAsync().ConfigureAwait(false) ?? string.Empty;
    }

    private StringContent PrepareRequest<T>(T obj)
    {
      var jsonRequest = !obj.Equals(default(T)) ? JsonConvert.SerializeObject(obj) : null;
      return new StringContent(jsonRequest, Encoding.UTF8, "application/json");
    }

  }
}