using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CosmosDbApp;
using Microsoft.Azure.Cosmos;
using CosmosDbApp.Models;
using CosmosDbApp.Services;
var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<ICosmosDbService, CosmosDbService>(); 
await builder.Build().RunAsync();
