using Newtonsoft.Json;
using System.Text;

namespace PremiseContractsTests.Extensions
{
    internal static class ClientExtensions
    {
        internal static async Task<HttpResponseMessage> PostJsonAsync<T>(this HttpClient client, string url, T contract)
        {
            var json = new StringContent(JsonConvert.SerializeObject(contract), Encoding.UTF8, "application/json");
            return await client.PostAsync(url, json);
        }
    }
}
