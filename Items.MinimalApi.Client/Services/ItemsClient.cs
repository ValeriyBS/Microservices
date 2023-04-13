using Items.Contracts.Responses;
using Items.MinimalApi.Client.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


namespace Items.MinimalApi.Client.Services
{
    public class ItemsClient : IItemsClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ItemsClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpClient.DefaultRequestHeaders.Add("X-Api-Key", _configuration.GetSection(Constants.ApiKey).Value);
            _httpClient.DefaultRequestHeaders.Add("Realm", _configuration.GetSection(Constants.OwnerName).Value);
        }
        public async Task<ItemsResponseDto> GetItems()
        {
            var url = _configuration.GetSection(Constants.Url).Value;
            return await JsonSerializer.DeserializeAsync<ItemsResponseDto>
                (await _httpClient.GetStreamAsync($"{url}/items"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
