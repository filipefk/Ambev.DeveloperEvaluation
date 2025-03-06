using System.Text.Json;

namespace TestUtil.HttpUtil;

public static class HttpResponseUtil
{
    public static async Task<JsonElement.ArrayEnumerator> GetErrorMessages(HttpResponseMessage response)
    {
        var responseData = await GetResponseData(response);

        return responseData.RootElement.GetProperty("errorMessages").EnumerateArray();
    }

    public static async Task<JsonDocument> GetResponseData(HttpResponseMessage response)
    {
        await using var responseBody = await response.Content.ReadAsStreamAsync();

        return await JsonDocument.ParseAsync(responseBody);
    }

}
