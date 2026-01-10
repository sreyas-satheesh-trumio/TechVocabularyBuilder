using System.Net.Http.Json;

namespace TechVocabulary.BlazorWasm.Services
{
    public class LearningApiService
    {
        private readonly HttpClient _http;

        public LearningApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<LearningTopicDto?> GetTopicByNameAsync(string topicName)
        {
            return await _http.GetFromJsonAsync<LearningTopicDto>(
                $"api/learning/topic?name={topicName}");
        }
    }
}
