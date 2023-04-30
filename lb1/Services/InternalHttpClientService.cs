using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using lb1.Services.Abstractions;
using Newtonsoft.Json;

namespace lb1.Services;

public class InternalHttpClientService : IInternalHttpClientService
{
    private readonly IHttpClientFactory _clientFactory;

    public InternalHttpClientService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<HttpResponseMessage> GetAsync<TRequest>(string url, HttpMethod method, TRequest content = null)
        where TRequest : class
    {
        var client = _clientFactory.CreateClient();

        var httpMessage = new HttpRequestMessage();
        httpMessage.RequestUri = new Uri(url);
        httpMessage.Method = method;

        if (content != null)
        {
            httpMessage.Content =
                new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

        }

        var result = await client.SendAsync(httpMessage);
        return result;
    }

    public async Task<TResponse> DeserializeResponse<TResponse>(HttpResponseMessage massage)
    {

        var resultContent = massage.Content.ReadAsStringAsync().Result;
        var response = JsonConvert.DeserializeObject<TResponse>(resultContent);
        return response!;
    }

    public async Task<TResponse> SendAsync<TResponse, TRequest>(string url, HttpMethod method, TRequest content = null)
        where TRequest : class
    {
        var result = await GetAsync<TRequest>(url, method, content);

        return await DeserializeResponse<TResponse>(result);
    }

   
}