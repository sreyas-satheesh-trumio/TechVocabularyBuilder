using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TechVocabulary.BlazorWasm;
using System.Net.Http;
using TechVocabulary.BlazorWasm.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Root components
builder.Services.AddScoped<GameStateService>();

builder.Services.AddScoped<JwtAuthHandler>();

builder.Services.AddScoped(sp =>
{
    var handler = sp.GetRequiredService<JwtAuthHandler>();

    handler.InnerHandler = new HttpClientHandler();

    return new HttpClient(handler)
    {
        BaseAddress = new Uri("http://localhost:5240/")
    };
});

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5240/") // <-- your backend API
});

builder.Services.AddScoped<AdminApiService>();
builder.Services.AddScoped<LearningApiService>();
builder.Services.AddScoped<GameStateService>();

builder.Services.AddScoped<IAuthApiService, AuthApiService>();

await builder.Build().RunAsync();
