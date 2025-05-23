namespace AnonPosters.API.Services.Tokens;

public interface ITokenService
{
    string CreateToken(string username);
    string CreateRefreshToken();
}