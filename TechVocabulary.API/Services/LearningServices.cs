// using Microsoft.EntityFrameworkCore;
  
// namespace TechVocabulary.API.Services
// {
//     public class LearningService : ILearningService
//     {
//         private readonly AppDbContext _context;

//         public LearningService(AppDbContext context)
//         {
//             _context = context;
//         }

//         // public async Task<LearningTopicDto> GetTopicByNameAsync(string topicName)
//         // {
//         //     var topic = await _context.Topics
//         //         .FirstOrDefaultAsync(t => t.TopicName.ToLower() == topicName.ToLower());

//         //     if (topic == null)
//         //         return null;

//         //     return new LearningTopicDto
//         //     {
//         //         TopicId = topic.TopicId,
//         //         TopicName = topic.TopicName,
//         //         Definition = topic.Definition,
//         //         RealWorldUsage = topic.RealWorldUsage,
//         //         CodeSnippet = topic.CodeSnippet
//         //     };
//         // }
// public async Task<LearningTopicDto?> GetTopicByNameAsync(string topicName)
// {
//     var normalized = topicName.Trim();

//     var topic = await _context.Topics
//         .AsNoTracking()
//         .FirstOrDefaultAsync(t => t.TopicName.Trim() == normalized);

//     if (topic == null)
//         return null;

//     return new LearningTopicDto
//     {
//         TopicId = topic.TopicId,
//         TopicName = topic.TopicName,
//         Definition = topic.Definition,
//         RealWorldUsage = topic.RealWorldUsage,
//         CodeSnippet = topic.CodeSnippet
//     };
// }



//     }
// }
// using Microsoft.EntityFrameworkCore;
  
// namespace TechVocabulary.API.Services  works this
// {
//     public class LearningService : ILearningService
//     {
//         private readonly AppDbContext _context;

//         public LearningService(AppDbContext context)
//         {
//             _context = context;
//         }

//         public async Task<LearningTopicDto> GetTopicByNameAsync(string topicName)
//         {
//             var topic = await _context.Topics
//                 .FirstOrDefaultAsync(t => t.TopicName.ToLower() == topicName.ToLower());

//             if (topic == null)
//                 return null;

//             return new LearningTopicDto
//             {
//                 TopicId = topic.TopicId,
//                 TopicName = topic.TopicName,
//                 Definition = topic.Definition,
//                 RealWorldUsage = topic.RealWorldUsage,
//                 CodeSnippet = topic.CodeSnippet
//             };
//         }
//     }
// }

using Microsoft.EntityFrameworkCore;
using TechVocabulary.Contracts.DTOs;

namespace TechVocabulary.API.Services
{
    public class LearningService : ILearningService
    {
        private readonly AppDbContext _context;

        public LearningService(AppDbContext context)
        {
            _context = context;
        }

        // 👉 Show only topic names (Discoverability)
        public async Task<List<LearningTopicListDto>> GetAllTopicsAsync()
        {
            return await _context.Topics
                .Select(t => new LearningTopicListDto
                {
                    TopicId = t.TopicId,
                    TopicName = t.TopicName
                })
                .ToListAsync();
        }

        // 👉 Full topic details
        public async Task<LearningTopicDto?> GetTopicByNameAsync(string topicName)
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

