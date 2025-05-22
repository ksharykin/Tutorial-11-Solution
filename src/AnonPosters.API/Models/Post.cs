using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AnonPosters.API.Models;

// Introduced with EF Core 7.0
[PrimaryKey(nameof(Id))]
public class Post
{
    public int Id { get; set; }
    
    [MaxLength(250)]
    public required string Content { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
}