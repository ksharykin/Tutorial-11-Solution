namespace AnonPosters.API.DTOs;

public class PostInfoDto
{
    public string Content { get; set; }
    public required UserDto User { get; set; }
    public DateTime CreatedAt { get; set; }
}