using TechVocabulary.Contracts.DTOs;
namespace TechVocabulary.API.Services
{
    public interface ILearningService
    {
         Task<List<LearningTopicListDto>> GetAllTopicsAsync();
        Task<LearningTopicDto> GetTopicByNameAsync(string topicName);
    }
}
