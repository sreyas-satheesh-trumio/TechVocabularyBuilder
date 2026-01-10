using Microsoft.AspNetCore.Mvc;
using TechVocabulary.API.Services;

[ApiController]
[Route("api/learning")]
public class LearningController : ControllerBase
{
    private readonly ILearningService _learningService;

    public LearningController(ILearningService learningService)
    {
        _learningService = learningService;
    }

    // GET: api/learning/topic?name=API
    [HttpGet("topic")]
    public async Task<IActionResult> GetTopic([FromQuery] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest(new { message = "Topic name is required" });

        var topic = await _learningService.GetTopicByNameAsync(name);

        if (topic == null)
            return NotFound(new { message = "Topic not found" });

        return Ok(topic);
    }
}
