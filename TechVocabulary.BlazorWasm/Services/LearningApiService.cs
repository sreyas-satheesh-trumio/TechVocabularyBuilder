// using System.Net.Http.Json;

// namespace TechVocabulary.BlazorWasm.Services
// {
//     public class LearningApiService
//     {
//         private readonly HttpClient _http;

//         public LearningApiService(HttpClient http)
//         {
//             _http = http;
//         }

//         public async Task<LearningTopicDto?> GetTopicByNameAsync(string topicName)
//         {
//             return await _http.GetFromJsonAsync<LearningTopicDto>(
//                 $"api/learning/topic?name={topicName}");
//         }
//     }
// }
using System.Net.Http.Json;
using TechVocabulary.Contracts.DTOs;

namespace TechVocabulary.BlazorWasm.Services
{
    public class LearningApiService
    {
        private readonly HttpClient _http;

        public LearningApiService(HttpClient http)
        {
            _http = http;
        }

        // 👉 Get all topic names
        public async Task<List<LearningTopicListDto>> GetAllTopicsAsync()
        {
            return await _http.GetFromJsonAsync<List<LearningTopicListDto>>(
                "api/learning/topics") ?? new();
        }

        // 👉 Get topic details
        public async Task<LearningTopicDto?> GetTopicByNameAsync(string topicName)
        {
            return await _http.GetFromJsonAsync<LearningTopicDto>(
                $"api/learning/topic?name={topicName}");
        }
    }
}
