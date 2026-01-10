using TechVocabulary.Contracts.AdminDTOs;

namespace TechVocabulary.API.Services
{
    public interface IAdminService
    {
        Task AddTopicAsync(CreateTopicDto dto, int adminId);
        Task<bool> DeleteTopicAsync(int topicId);
        Task<QuestionDto> GetRandomQuestionAsync();
    }
}
