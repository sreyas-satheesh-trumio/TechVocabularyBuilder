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
        // Get already attempted topic IDs for the user
        var attemptedTopicIds = _db.GameProgresses
            .Where(g => g.UserId == userId)
            .Select(g => g.TopicId)
            .ToList();

        // Get next unattempted topic
        var topic = _db.Topics
            .Where(t => !attemptedTopicIds.Contains(t.TopicId))
            .OrderBy(t => Guid.NewGuid())
            .FirstOrDefault();

        // If no topics left
        if (topic == null)
        {
            return new GameQuestionResponse
            {
                IsCompleted = true
            };
        }

        // Build options (topic names)
        var options = _db.Topics
            .OrderBy(t => Guid.NewGuid())
            .Take(4)
            .Select(t => t.TopicName)
            .ToList();

        // Ensure correct answer is included
        if (!options.Contains(topic.TopicName))
        {
            options[0] = topic.TopicName;
        }

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
            throw new Exception("Invalid topic");

        // Prevent duplicate attempts
        bool alreadyAttempted = _db.GameProgresses.Any(g =>
            g.UserId == userId && g.TopicId == request.TopicId);

        if (alreadyAttempted)
            throw new Exception("Topic already attempted");

        bool isCorrect = topic.TopicName
            .Equals(request.SelectedAnswer, StringComparison.OrdinalIgnoreCase);

        int score = isCorrect ? 1 : 0;

        // Save attempt
        _db.GameProgresses.Add(new GameProgress
        {
            UserId = userId,
            TopicId = topic.TopicId,
            Score = score,
            AttemptedAt = DateTime.UtcNow
        });

        // If correct, mark as learned
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

        int totalQuestions = attempts.Count;
        int correctAnswers = attempts.Count(a => a.Score > 0);
        int totalScore = correctAnswers; // 1 mark per correct answer

        return new GameScoreResponse
        {
            TotalQuestions = totalQuestions,
            CorrectAnswers = correctAnswers,
            TotalScore = totalScore
        };
    }
}
