using System.ComponentModel.DataAnnotations;
using AnonPosters.API.Enums;
using Microsoft.EntityFrameworkCore;

namespace AnonPosters.API.Models;

[PrimaryKey(nameof(Id))]
public class User
{
    public int Id { get; set; }
    
    [MaxLength(50)]
    public required string Username { get; set; }
    
    [MaxLength(100)]
    public required string Password { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public Role Role { get; set; }
    
    public ICollection<Post> Posts { get; set; } = new List<Post>();
}