using System.ComponentModel.DataAnnotations;

namespace AnonPosters.API.DTOs.Posts;

public class CreatePostDto
{
    [Required]
    public required string Content { get; set; }
    
    [Required]
    public int UserId { get; set; }
}