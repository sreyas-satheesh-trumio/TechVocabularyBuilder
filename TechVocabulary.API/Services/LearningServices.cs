using Microsoft.EntityFrameworkCore;
  
namespace TechVocabulary.API.Services
{
    public class LearningService : ILearningService
    {
        private readonly AppDbContext _context;

        public LearningService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LearningTopicDto> GetTopicByNameAsync(string topicName)
        {
            var topic = await _context.Topics
                .FirstOrDefaultAsync(t => t.TopicName.ToLower() == topicName.ToLower());

            if (topic == null)
                return null;

            return new LearningTopicDto
            {
                TopicId = topic.TopicId,
                TopicName = topic.TopicName,
                Definition = topic.Definition,
                RealWorldUsage = topic.RealWorldUsage,
                CodeSnippet = topic.CodeSnippet
            };
        }
    }
}
