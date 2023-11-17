using System.IdentityModel.Tokens.Jwt;

namespace Module_02.Task_01.CartingService.WebApi.Middleware;

public class JwtAccessTokenLoggerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;
    private readonly string _line = string.Concat(Enumerable.Repeat("-", 80));

    public JwtAccessTokenLoggerMiddleware(RequestDelegate next, ILogger<JwtAccessTokenLoggerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var authorizationHeader = context.Request.Headers.Authorization.FirstOrDefault();

        if (authorizationHeader != null && 
            authorizationHeader.StartsWith("Bearer", StringComparison.InvariantCultureIgnoreCase) &&
            context.User.Identity.IsAuthenticated)
        {
            var jwt = authorizationHeader.Split(" ")[1];
            var token = new JwtSecurityToken(jwt);
            var payloadJson = token.Payload.SerializeToJson();
            _logger.LogInformation($"\n{context.User.Identity.Name}\n{_line}\n{payloadJson}\n{_line}\n");
        }
        else
        {
            _logger.LogInformation("Not authenticated request.");
        }


        await _next(context);
    }
}

public static class JwtAccessTokenLoggerMiddlewareExtensions
{
    public static IApplicationBuilder UseJwtAccessTokenLogger(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<JwtAccessTokenLoggerMiddleware>();
    }
}