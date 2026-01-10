using TechVocabulary.Contracts.DTOs;

public class GameStateService
{
    public GameQuestionResponse? CurrentQuestion { get; set; }
    public int CurrentScore { get; set; } = 0;
    public bool IsGameCompleted { get; set; } = false;

    public void Reset()
    {
        CurrentQuestion = null;
        CurrentScore = 0;
        IsGameCompleted = false;
    }
}
