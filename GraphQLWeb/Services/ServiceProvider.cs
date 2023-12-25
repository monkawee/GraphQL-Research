using Newtonsoft.Json;
using System.Text;

namespace GraphQLWeb.Services
{
    public interface ILocalServiceProvider
    {
        Task<T?> GetAsync<T>(string uri);
        Task<T?> PostAsync<T>(string uri, object param);
    }

    public class LocalServiceProvider : ILocalServiceProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LocalServiceProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<T?> GetAsync<T>(string uri)
        {
            T? result = default;

            try
            {
                var client = _httpClientFactory.CreateClient($"");

                using (var httpResponse = await client.GetAsync(uri))
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        string jsonString = await httpResponse.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<T>(jsonString);
                    }
                }
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<T?> PostAsync<T>(string uri, object param)
        {
            T? result = default;

            try
            {
                var client = _httpClientFactory.CreateClient($"");
                string jsonParam = JsonConvert.SerializeObject(param);
                StringContent stringContent = new StringContent(jsonParam, Encoding.UTF8, "application/json");

                using (var httpResponse = await client.PostAsync(uri, stringContent))
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        string jsonString = await httpResponse.Content.ReadAsStringAsync();

                        result = JsonConvert.DeserializeObject<T>(jsonString);
                    }
                }
            }
            catch
            {
                throw;
            }

            return result;
        }
    }
}
