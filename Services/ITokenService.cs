using System.Security.Claims;
using DotnetIdentityDemo.Models;

namespace DotnetIdentityDemo.Services;

public interface ITokenService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken(User user);
    ClaimsPrincipal? ValidateRefreshToken(string refreshToken);
}