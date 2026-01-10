using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/game")]
[Authorize(Roles = "User")]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet("question")]
    public IActionResult GetQuestion()
    {
        int userId = int.Parse(User.FindFirst("UserId").Value);
        return Ok(_gameService.GetNextQuestion(userId));
    }

    [HttpPost("answer")]
    public IActionResult ValidateAnswer([FromBody] AnswerRequest request)
    {
        int userId = int.Parse(User.FindFirst("UserId").Value);
        return Ok(_gameService.ValidateAnswer(userId, request));
    }

    [HttpGet("score")]
    public IActionResult GetScore()
    {
        int userId = int.Parse(User.FindFirst("UserId").Value);
        return Ok(_gameService.CalculateScore(userId));
    }
}
