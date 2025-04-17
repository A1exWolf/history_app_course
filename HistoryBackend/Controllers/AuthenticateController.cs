using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HistoryBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HistoryBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticateController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    // private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }


    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
        
        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"], // Адрес сервера
            audience: _configuration["JWT:ValidAudience"], // Адрес клиента
            expires: DateTime.Now.AddHours(3), // Время жизни токена
            claims: authClaims, // Параметры токена
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature) // Алгоритм шифрования
        );

        return token;
    }
}