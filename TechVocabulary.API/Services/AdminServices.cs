// 
// using Microsoft.EntityFrameworkCore;
// using TechVocabulary.Contracts.AdminDTOs;
// namespace TechVocabulary.API.Services
// {
// public class AdminService : IAdminService
// {
//     private readonly AppDbContext _context;
//      private readonly Random _random = new(); 
//     public AdminService(AppDbContext context)
//     {
//         _context = context;
//     }

//    public async Task AddTopicAsync(CreateTopicDto dto, int adminId)
// {
//     var topic = new Topic
//     {
//         TopicName = dto.TopicName,
//         Definition = dto.Definition,
//         RealWorldUsage = dto.RealWorldUsage,
//         CodeSnippet = dto.CodeSnippet,
//         CreatedBy = 1,
//         CreatedAt = DateTime.UtcNow
//     };

//     _context.Topics.Add(topic);
//     await _context.SaveChangesAsync();
// }
//  public async Task<bool> DeleteTopicAsync(int topicId)
//     {
//         var topic = await _context.Topics
//             .FirstOrDefaultAsync(t => t.TopicId == topicId);

//         if (topic == null)
//             return false;

//         _context.Topics.Remove(topic);
//         await _context.SaveChangesAsync();

//         return true;
//     }
//       public async Task<QuestionDto> GetRandomQuestionAsync()
//     {
    
//         var topics = await _context.Topics.ToListAsync();

//         if (!topics.Any())
//             return null;

//         // Pick a random topic as the "question"
//         var questionTopic = topics[_random.Next(topics.Count)];

//         // Pick 3 more random topics as wrong options
//         var otherTopics = topics
//             .Where(t => t.TopicId != questionTopic.TopicId)
//             .OrderBy(_ => _random.Next())
//             .Take(3)
//             .Select(t => t.TopicName)
//             .ToList();

//         // Combine correct + wrong options and shuffle
//         var options = otherTopics.Append(questionTopic.TopicName).OrderBy(_ => _random.Next()).ToList();

//         return new QuestionDto
//         {
//             CodeSnippet = questionTopic.CodeSnippet,
//             Options = options,
//             CorrectOption = questionTopic.TopicName
//         };
//     }
//        public async Task<LearningTopicDto> GetTopicByNameAsync(string topicName)
//     {
//         var topic = await _context.Topics
//             .FirstOrDefaultAsync(t => t.TopicName.ToLower() == topicName.ToLower());

//         if (topic == null)
//             return null;

//         return new LearningTopicDto
//         {
//             TopicId = topic.TopicId,
//             TopicName = topic.TopicName,
//             Definition = topic.Definition,
//             RealWorldUsage = topic.RealWorldUsage,
//             CodeSnippet = topic.CodeSnippet
//         };
//     }

// }
// }
using Microsoft.EntityFrameworkCore;
using TechVocabulary.Contracts.AdminDTOs;
namespace TechVocabulary.API.Services
{
public class AdminService : IAdminService
{
    private readonly AppDbContext _context;
     private readonly Random _random = new(); 
    public AdminService(AppDbContext context)
    {
        _context = context;
    }

   public async Task AddTopicAsync(CreateTopicDto dto, int adminId)
{
    var topic = new Topic
    {
        TopicName = dto.TopicName,
        Definition = dto.Definition,
        RealWorldUsage = dto.RealWorldUsage,
        CodeSnippet = dto.CodeSnippet,
        CreatedBy = 1,
        CreatedAt = DateTime.UtcNow
    };

    _context.Topics.Add(topic);
    await _context.SaveChangesAsync();
}
 public async Task<bool> DeleteTopicAsync(int topicId)
    {
        var topic = await _context.Topics
            .FirstOrDefaultAsync(t => t.TopicId == topicId);

        if (topic == null)
            return false;

        _context.Topics.Remove(topic);
        await _context.SaveChangesAsync();

        return true;
    }
      public async Task<QuestionDto> GetRandomQuestionAsync()
    {
    
        var topics = await _context.Topics.ToListAsync();

        if (!topics.Any())
            return null;

        // Pick a random topic as the "question"
        var questionTopic = topics[_random.Next(topics.Count)];

        // Pick 3 more random topics as wrong options
        var otherTopics = topics
            .Where(t => t.TopicId != questionTopic.TopicId)
            .OrderBy(_ => _random.Next())
            .Take(3)
            .Select(t => t.TopicName)
            .ToList();

        // Combine correct + wrong options and shuffle
        var options = otherTopics.Append(questionTopic.TopicName).OrderBy(_ => _random.Next()).ToList();

        return new QuestionDto
        {
            CodeSnippet = questionTopic.CodeSnippet,
            Options = options,
            CorrectOption = questionTopic.TopicName
        };
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