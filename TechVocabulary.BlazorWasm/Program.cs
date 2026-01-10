using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TechVocabulary.BlazorWasm;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<GameStateService>();

// ✅ ONLY ONE HttpClient — pointing to API
builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri("http://localhost:5240/")
    });

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IAuthApiService, AuthApiService>();

await builder.Build().RunAsync();
