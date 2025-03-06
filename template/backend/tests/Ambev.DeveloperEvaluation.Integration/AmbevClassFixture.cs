using System.Net.Http.Headers;
using System.Net.Http.Json;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration;

public class AmbevClassFixture : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public AmbevClassFixture(CustomWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
    }

    protected async Task<HttpResponseMessage> DoPost(string url, object request, string token = "", string culture = "pt-BR")
    {
        ChangeRequestCulture(culture);
        AddAuthorization(token);
        return await _httpClient.PostAsJsonAsync(url, request);
    }

    protected async Task<HttpResponseMessage> DoGet(string url, string token = "", string culture = "pt-BR")
    {
        ChangeRequestCulture(culture);
        AddAuthorization(token);
        return await _httpClient.GetAsync(url);
    }

    protected async Task<HttpResponseMessage> DoDelete(string url, string token = "", string culture = "pt-BR")
    {
        ChangeRequestCulture(culture);
        AddAuthorization(token);
        return await _httpClient.DeleteAsync(url);
    }

    protected async Task<HttpResponseMessage> DoPut(string url, object request, string token = "", string culture = "pt-BR")
    {
        ChangeRequestCulture(culture);
        AddAuthorization(token);
        return await _httpClient.PutAsJsonAsync(url, request);
    }

    private void ChangeRequestCulture(string culture)
    {
        AddHeader("Accept-Language", culture);
    }

    private void AddAuthorization(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return;

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    protected void AddHeader(string key, string value)
    {
        if (_httpClient.DefaultRequestHeaders.Contains(key))
            _httpClient.DefaultRequestHeaders.Remove(key);

        _httpClient.DefaultRequestHeaders.Add(key, value);
    }
}
