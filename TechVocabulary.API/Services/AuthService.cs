using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechVocabulary.API.Models;
using TechVocabulary.API.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly JwtOptions _jwt;

    public AuthService(AppDbContext context, IOptions<JwtOptions> jwt)
    {
        _context = context;
        _jwt = jwt.Value;
    }

    public async Task<string?> LoginAsync(string username, string password)
    {
        var user = await _context.EndUsers
            .FirstOrDefaultAsync(u => u.Username == username);

        if (user == null)
            return null;

        if (!PasswordService.Verify(password, user.Password))
            return null;

        return GenerateToken(user);
    }

    private string GenerateToken(EndUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwt.Key)
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwt.ExpiresInMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
