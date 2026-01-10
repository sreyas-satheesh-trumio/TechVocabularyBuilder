using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

[ApiController]
[Route("api/game")]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    private int GetUserId()
    {
        return 1; // TEMP
    }

    [HttpGet("question")]
    public IActionResult GetQuestion()
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
            ?? throw new UnauthorizedAccessException("User id not found in token"));
        return Ok(_gameService.GetNextQuestion(userId));
    }

    [HttpPost("answer")]
    public IActionResult ValidateAnswer([FromBody] AnswerRequest request)
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
            ?? throw new UnauthorizedAccessException("User id not found in token"));
        return Ok(_gameService.ValidateAnswer(userId, request));
    }

    [HttpGet("score")]
    public IActionResult GetScore()
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
            ?? throw new UnauthorizedAccessException("User id not found in token"));
        return Ok(_gameService.CalculateScore(userId));
    }
}

