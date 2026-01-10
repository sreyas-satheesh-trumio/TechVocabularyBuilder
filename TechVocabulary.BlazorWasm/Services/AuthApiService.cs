using System.Net.Http.Json;
public class AuthApiService : IAuthApiService
{
    private readonly HttpClient _http;

    public AuthApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<bool> RegisterAsync(RegisterRequest request)
    {
        try
        {
            var response = await _http.PostAsJsonAsync(
                "/api/auth/register", request);
            return true;
            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<string?> LoginAsync(LoginRequest request)
    {
        var response = await _http.PostAsJsonAsync(
            "api/auth/login", request);

        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content
            .ReadFromJsonAsync<LoginResponse>();

        return result?.AccessToken;
    }
}
