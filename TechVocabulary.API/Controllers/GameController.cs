using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


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
       

        return Ok(_gameService.GetNextQuestion(GetUserId()));
    }

    
    [HttpPost("answer")]
    public IActionResult ValidateAnswer([FromBody] AnswerRequest request)
    {

        return Ok(_gameService.ValidateAnswer(GetUserId(), request));
    }

   
    [HttpGet("score")]
    public IActionResult GetScore()
    {

        return Ok(_gameService.CalculateScore(GetUserId()));
    }
}

