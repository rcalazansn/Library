using System.Text.Json;

namespace Library.Application.Http.ViaCep.HttpClientFactory
{
    public class ViaCepClientFactory : IViaCepClientFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ViaCepClientFactory(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<ViaCepResponse> GetAddressAsync(string cep)
        {
            HttpClient client = _httpClientFactory.CreateClient(typeof(ViaCepClientFactory).Name);

            HttpResponseMessage ? response = await client.GetAsync($"/ws/{cep}/json");

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                var viaCepResponse = JsonSerializer.Deserialize<ViaCepResponse>(responseBody);

                return viaCepResponse;
            }
            else
                return null;
        }
    }
}
