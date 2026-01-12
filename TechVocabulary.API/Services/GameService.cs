using Microsoft.EntityFrameworkCore;
using TechVocabulary.Contracts.DTOs;

public class GameService : IGameService
{
    private readonly AppDbContext _db;

    public GameService(AppDbContext db)
    {
        _db = db;
    }

    // -----------------------------
    // GET NEXT QUESTION
    // -----------------------------
    public GameQuestionResponse GetNextQuestion(int userId)
    {
        var attemptedTopicIds = _db.GameProgresses
            .Where(g => g.UserId == userId)
            .Select(g => g.TopicId)
            .ToList();

        var remainingTopics = _db.Topics
            .Where(t => !attemptedTopicIds.Contains(t.TopicId))
            .ToList();

        // No questions left
        if (!remainingTopics.Any())
        {
            return new GameQuestionResponse
            {
                IsCompleted = true
            };
        }

        var topic = remainingTopics
            .OrderBy(_ => Guid.NewGuid())
            .First();

        // Build option list safely
        var options = _db.Topics
            .Select(t => t.TopicName)
            .Distinct()
            .OrderBy(_ => Guid.NewGuid())
            .Take(3)
            .ToList();

        // Ensure correct answer is included
        if (!options.Contains(topic.TopicName))
        {
            options.Add(topic.TopicName);
        }

        // Shuffle again
        options = options
            .OrderBy(_ => Guid.NewGuid())
            .ToList();

        return new GameQuestionResponse
        {
            TopicId = topic.TopicId,
            Definition = topic.Definition,
            RealWorldUsage = topic.RealWorldUsage,
            CodeSnippet = topic.CodeSnippet,
            Options = options,
            IsCompleted = false
        };
    }

    // -----------------------------
    // VALIDATE ANSWER
    // -----------------------------
    public AnswerResultResponse ValidateAnswer(int userId, AnswerRequest request)
    {
        var topic = _db.Topics.FirstOrDefault(t => t.TopicId == request.TopicId);
        if (topic == null)
        {
            return new AnswerResultResponse
            {
                IsCorrect = false
            };
        }

        bool alreadyAttempted = _db.GameProgresses.Any(g =>
            g.UserId == userId && g.TopicId == request.TopicId);

        if (alreadyAttempted)
        {
            return new AnswerResultResponse
            {
                IsCorrect = false
            };
        }

        bool isCorrect = topic.TopicName.Equals(
            request.SelectedAnswer,
            StringComparison.OrdinalIgnoreCase);

        _db.GameProgresses.Add(new GameProgress
        {
            UserId = userId,
            TopicId = topic.TopicId,
            Score = isCorrect ? 1 : 0,
            AttemptedAt = DateTime.UtcNow
        });

        if (isCorrect)
        {
            _db.TopicsLearned.Add(new TopicLearned
            {
                UserId = userId,
                TopicId = topic.TopicId,
                LearnedAt = DateTime.UtcNow
            });
        }

        _db.SaveChanges();

        return new AnswerResultResponse
        {
            IsCorrect = isCorrect
        };
    }

    // -----------------------------
    // CALCULATE FINAL SCORE
    // -----------------------------
    public GameScoreResponse CalculateScore(int userId)
    {
        var attempts = _db.GameProgresses
            .Where(g => g.UserId == userId)
            .ToList();

        return new GameScoreResponse
        {
            TotalQuestions = attempts.Count,
            CorrectAnswers = attempts.Count(a => a.Score > 0),
            TotalScore = attempts.Count(a => a.Score > 0)
        };
    }
}
