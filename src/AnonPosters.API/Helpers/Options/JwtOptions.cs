namespace AnonPosters.API.Helpers.Options;

public class JwtOptions
{
    public const string ConfigKey = "Jwt";
    
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required string Key { get; set; }
}