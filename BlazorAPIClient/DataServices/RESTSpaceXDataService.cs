using BlazorAPIClient.Dtos;
using System.Net.Http.Json;

namespace BlazorAPIClient.DataServices
{
    public class RESTSpaceXDataService : ISpaceXDataService
    {
        private readonly HttpClient _httpClient;

        public RESTSpaceXDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<LaunchDto[]?> GetAllLaunches()
        {
            Console.WriteLine("--> REST SpaceX Data Service: Getting launchers...");
            return await _httpClient.GetFromJsonAsync<LaunchDto[]>("/rest/launches");
        }
    }
}