using TechVocabulary.Contracts.DTOs;
public interface IGameService
{
    GameQuestionResponse GetNextQuestion(int userId);
    AnswerResultResponse ValidateAnswer(int userId, AnswerRequest request);
    GameScoreResponse CalculateScore(int userId);
}
