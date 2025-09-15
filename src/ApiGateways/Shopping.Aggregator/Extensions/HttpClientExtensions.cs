using System.Text.Json;

namespace Shopping.Aggregator.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"something went wrong while calling api:{response.ReasonPhrase}");
            }
            var dataAsString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(dataAsString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
