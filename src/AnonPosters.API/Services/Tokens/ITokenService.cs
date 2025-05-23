namespace AnonPosters.API.Services.Tokens;

public interface ITokenService
{
    void CreateToken(string username);
    void RefreshToken(string token);
}