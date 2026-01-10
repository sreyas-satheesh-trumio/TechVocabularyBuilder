using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TechVocabulary.BlazorWasm;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

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

builder.Services.AddScoped<IAuthApiService, AuthApiService>();

await builder.Build().RunAsync();
