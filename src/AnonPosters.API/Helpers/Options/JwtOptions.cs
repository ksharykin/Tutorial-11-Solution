using Microsoft.Extensions.Options;

namespace AnonPosters.API.Helpers.Options;

public class JwtOptions
{
    public const string ConfigKey = "Jwt";
    
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string Key { get; init; }
    public required int ValidInMinutes { get; init; }
}