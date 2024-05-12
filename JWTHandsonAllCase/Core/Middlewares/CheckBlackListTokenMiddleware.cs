using JWTHandsonAllCase.Common;
using JWTHandsonAllCase.Repository.TokenServicesRepository;

namespace JWTHandsonAllCase.Core.Middlewares
{
    public class CheckBlackListTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CheckBlackListTokenMiddleware> _logger;
        private readonly IServiceScopeFactory _blacklistService;

        public CheckBlackListTokenMiddleware(RequestDelegate request, ILogger<CheckBlackListTokenMiddleware> logger, IServiceScopeFactory services)
        {

            _next = request;
            _logger = logger;
            _blacklistService = services;
        }
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            bool IsRevoked = false;

            if (token != null)
            {
                using(var scope = _blacklistService.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<TokenRevocationServices>();
                    if (_context.IsTokenRevoked(token).Result)
                    {
                        IsRevoked = true;

                    }
                }
               
            }
            if (IsRevoked)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            await _next(context);
        }
    }
}
