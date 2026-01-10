using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TechVocabulary.BlazorWasm;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Root components
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient for calling the API (Backend URL)
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5240/") // <-- your backend API
});

// Register AdminApiService
builder.Services.AddScoped<AdminApiService>();

await builder.Build().RunAsync();
