using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System.IdentityModel.Tokens.Jwt;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;

    public TokenValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IUserRepository userRepository)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var token = context.Request.Headers["Authorization"].ToString().Split(" ").Last();
            var jwtToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
            var userIdClaim = jwtToken?.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;

            if (Guid.TryParse(userIdClaim, out var userId))
            {
                var user = await userRepository.GetByIdAsync(userId);
                if (user == null || user.Status != UserStatus.Active)
                {
                    throw new UnauthorizedException("User is not active");
                }
            }
        }

        await _next(context);
    }
}
