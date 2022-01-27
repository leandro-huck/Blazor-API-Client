using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorAPIClient;
using BlazorAPIClient.DataServices;

Console.WriteLine("--> BlazorAPIClient has started...");

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration?["api_base_url"] ?? "") });


// If you want to make a REST request, change GraphQLSpaceXDataService for RESTSpaceXDataService
//builder.Services.AddHttpClient<ISpaceXDataService, RESTSpaceXDataService>
builder.Services.AddHttpClient<ISpaceXDataService, GraphQLSpaceXDataService>
    (spds => spds.BaseAddress = new Uri(builder.Configuration?["api_base_url"] ?? ""));

await builder.Build().RunAsync();
