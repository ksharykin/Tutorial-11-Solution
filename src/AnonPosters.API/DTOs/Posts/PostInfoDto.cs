using AnonPosters.API.DTOs.Users;

namespace AnonPosters.API.DTOs.Posts;

public class PostInfoDto
{
    public string Content { get; set; }
    public required UserDto User { get; set; }
    public DateTime CreatedAt { get; set; }
}