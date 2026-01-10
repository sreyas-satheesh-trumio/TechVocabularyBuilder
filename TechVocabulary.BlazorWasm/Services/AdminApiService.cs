using System.Net.Http.Json;
using TechVocabulary.Contracts.AdminDTOs;

public class AdminApiService
{
    private readonly HttpClient _http;

    public AdminApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task AddTopicAsync(CreateTopicDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/admin/topic", dto);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteTopicAsync(int topicId)
    {
        var response = await _http.DeleteAsync($"api/admin/topic/{topicId}");
        response.EnsureSuccessStatusCode();
    }
}
