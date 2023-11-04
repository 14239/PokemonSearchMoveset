using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PokemonSearchMoveset;
using PokemonSearchMoveset.Services;
using MudBlazor.Services;
using BlazorPanzoom;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<DataService>();
builder.Services.AddScoped<MoveInfoService>();

builder.Services.AddMudServices();
builder.Services.AddBlazorPanzoomServices();
await builder.Build().RunAsync();
