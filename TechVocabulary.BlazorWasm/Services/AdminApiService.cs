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
    try
    {
        // Make the POST request
        var response = await _http.PostAsJsonAsync("api/admin/topic", dto);

        // Read response content as string for debugging
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            // Log the status code and response
            Console.WriteLine($"Error adding topic. Status: {response.StatusCode}, Response: {content}");
            return;
        }

        Console.WriteLine("Topic added successfully!");
    }
    catch (HttpRequestException ex)
    {
        // Handle network/connection issues
        Console.WriteLine($"HTTP Request error: {ex.Message}");
    }
    catch (Exception ex)
    {
        // Handle any other errors
        Console.WriteLine($"Unexpected error: {ex.Message}");
    }
}


    public async Task DeleteTopicAsync(int topicId)
    {
        var response = await _http.DeleteAsync($"api/admin/topic/{topicId}");
        response.EnsureSuccessStatusCode();
    }
}
