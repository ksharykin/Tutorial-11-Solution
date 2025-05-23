using AnonPosters.API.DAL;
using AnonPosters.API.DTOs.Tokens;
using AnonPosters.API.DTOs.Users;
using AnonPosters.API.Models;
using AnonPosters.API.Services.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnonPosters.API.Controllers;

[Route("api/auth")]
[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly AnonPostersContext _context;
    private readonly ITokenService _tokenService;
    private readonly PasswordHasher<User> _passwordHasher = new();
    public AuthController(AnonPostersContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }
    [HttpPost]
    public async Task<IActionResult> Login(UserCredentialsDto userCredentials, CancellationToken cancellationToken)
    {
        var foundUser = await _context.User.FirstOrDefaultAsync(u => string.Equals(u.Username, userCredentials.Username),
            cancellationToken);
        if (foundUser == null)
        {
            return Unauthorized();
        }
            
        var verificationResult = 
            _passwordHasher.VerifyHashedPassword(foundUser, foundUser.Password, userCredentials.Password);
        if (verificationResult == PasswordVerificationResult.Failed)
        {
            return Unauthorized();
        }

        var tokens = new TokenDto
        {
            AccessToken = _tokenService.CreateToken(foundUser.Username),
            RefreshToken = _tokenService.CreateRefreshToken()
        };
            
        return Ok(tokens);
    }
}