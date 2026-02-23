using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers;

[ApiController]
[Route("api/account")]
public class AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IOptions<JwtSettings> jwtSettings) : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> Login(LoginUserViewModel loginUserViewModel)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(loginUserViewModel.Email, loginUserViewModel.Password, false, true);

        return result.Succeeded ? Ok(GenerateJwt(loginUserViewModel.Email)) : BadRequest(new { message = "Invalid username or password" });
    }

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult> Register(RegisterUserViewModel registerUserViewModel)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        IdentityUser user = new()
        {
            UserName = registerUserViewModel.Email,
            Email = registerUserViewModel.Email,
            EmailConfirmed = true
        };

        IdentityResult result = await _userManager.CreateAsync(user, registerUserViewModel.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return Ok(GenerateJwt(user.Email));
        }

        return BadRequest(new { message = "User registration failed", errors = result.Errors });
    }

    private string GenerateJwt(string email)
    {
        IdentityUser? user = _userManager.FindByEmailAsync(email).Result;
        IList<string> roles = _userManager.GetRolesAsync(user).Result;

        List<Claim> claims =
        [
            new Claim(ClaimTypes.Name, user.UserName)
        ];

        foreach (string role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        JwtSecurityTokenHandler tokenHandler = new();
        byte[] key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

        SecurityToken token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpirationHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        string encodedToken = tokenHandler.WriteToken(token);

        return encodedToken;
    }
}