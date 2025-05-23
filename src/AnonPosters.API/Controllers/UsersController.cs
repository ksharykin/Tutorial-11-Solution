using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnonPosters.API.DAL;
using AnonPosters.API.DTOs.Users;
using AnonPosters.API.Enums;
using AnonPosters.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AnonPosters.API.Controllers;

[Route("api/users")]
[Authorize]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly PasswordHasher<User> _passwordHasher = new();
        
    private readonly AnonPostersContext _context;

    public UsersController(AnonPostersContext context)
    {
        _context = context;
    }

    // GET: api/Users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUser()
    {
        return await _context.User.ToListAsync();
    }

    // GET: api/Users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _context.User.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    // PUT: api/Users/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Users
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<UserDto>> PostUser(UserCredentialsDto userCredentials)
    {
        var user = new User
        {
            Username = userCredentials.Username,
            Password = userCredentials.Password,
            CreatedAt = DateTime.Now,
            Role = Role.User
        };

        user.Password = _passwordHasher.HashPassword(user, userCredentials.Password);
            
        _context.User.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetUser", new { id = user.Id }, new UserDto {Username = user.Username, Id = user.Id});
    }

    // DELETE: api/Users/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.User.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.User.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserExists(int id)
    {
        return _context.User.Any(e => e.Id == id);
    }
}