using Microsoft.AspNetCore.Mvc;
using TechVocabulary.API.Services;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var token = await _authService.LoginAsync(
            request.Username,
            request.Password
        );

        if (token == null)
            return Unauthorized("Invalid username or password");

        return Ok(new LoginResponse
        {
            AccessToken = token
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var token = await _authService.RegisterAsync(request);

        if (token == null)
            return Conflict("Username or email already in use");

        return Created(string.Empty, new RegisterResponse
        {
            AccessToken = token
        });
    }
}
