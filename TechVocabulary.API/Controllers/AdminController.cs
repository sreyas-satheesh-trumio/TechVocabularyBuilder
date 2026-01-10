using Microsoft.AspNetCore.Mvc;
using TechVocabulary.API.Services;
using TechVocabulary.Contracts.AdminDTOs;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    // POST: api/admin/topic
    [HttpPost("topic")]
    public async Task<IActionResult> AddTopic([FromBody] CreateTopicDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // TEMP: hardcoded adminId OR pass from UI
        int adminId = dto.AdminUserId; // 👈 simple & clean

        await _adminService.AddTopicAsync(dto, adminId);

        return Ok(new { message = "Topic added successfully" });
    }
    [HttpDelete("topic/{topicId}")]
    public async Task<IActionResult> DeleteTopic(int topicId)
    {
        if (topicId <= 0)
            return BadRequest("Invalid topic ID");

        var deleted = await _adminService.DeleteTopicAsync(topicId);

        if (!deleted)
            return NotFound(new { message = "Topic not found" });

        return Ok(new { message = "Topic deleted successfully" });
    }
    [HttpGet("question")]
    public async Task<IActionResult> GetQuestion()
    {
        var question = await _adminService.GetRandomQuestionAsync();

        if (question == null)
            return NotFound(new { message = "No topics available" });

        return Ok(question);
    }
    
}
