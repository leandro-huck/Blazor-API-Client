using System.Text;
using System.Text.Json;
using BlazorAPIClient.Dtos;

namespace BlazorAPIClient.DataServices
{
    public class GraphQLSpaceXDataService : ISpaceXDataService
    {
        private readonly HttpClient _httpClient;

        public GraphQLSpaceXDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<LaunchDto[]>? GetAllLaunches()
        {
            Console.WriteLine("--> GraphQL SpaceX Data Service: Getting launchers...");
            var queryObject = new {
                query = @"{ launches{ id is_tentative mission_name launch_date_local } }",
                variables = new {}
            };

            var launchQuery = new StringContent(
                JsonSerializer.Serialize(queryObject),
                 Encoding.UTF8,
                 "application/json");
            
            var response = await _httpClient.PostAsync("/graphql", launchQuery);
            
            if (response.IsSuccessStatusCode)
            {
                var gqlData = await JsonSerializer.DeserializeAsync<GraphQLData>
                    (await response.Content.ReadAsStreamAsync());
                if (gqlData != null && gqlData.Data != null)
                {
                    return gqlData.Data.Launches;
                }                
            }
            return null;
        }
    }
}