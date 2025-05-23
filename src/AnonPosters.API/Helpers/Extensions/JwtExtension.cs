using System.Text;
using AnonPosters.API.Helpers.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AnonPosters.API.Helpers.Extensions;

public static class JwtExtension
{
    private static JwtOptions _jwtOptions = null!;
    
    public static IServiceCollection AddJwt(this IServiceCollection services, JwtOptions jwtOptions)
    {
        _jwtOptions = jwtOptions;
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = _jwtOptions.Issuer,
                    ValidAudience = _jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key)),
                    ClockSkew = TimeSpan.FromMinutes(_jwtOptions.ValidInMinutes)
                };
            });
        
        return services;
    }
}