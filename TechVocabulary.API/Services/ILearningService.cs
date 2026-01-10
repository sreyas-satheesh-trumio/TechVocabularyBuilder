
namespace TechVocabulary.API.Services
{
    public interface ILearningService
    {
        Task<LearningTopicDto> GetTopicByNameAsync(string topicName);
    }
}
