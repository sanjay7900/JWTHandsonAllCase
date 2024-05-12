using JWTHandsonAllCase.DBContextManager;

namespace JWTHandsonAllCase.Common
{
    public class TokenRevocationServices
    {
        private readonly ILogger<TokenRevocationServices> _logger;
        private readonly JWTDbContext _Context;

        public TokenRevocationServices(JWTDbContext jWTDbContext, ILogger<TokenRevocationServices> logger)
        {
            _logger = logger;
            _Context = jWTDbContext;

        }
        public  Task<bool> IsTokenRevoked(string token)
        {
            var IsExist = _Context.RevokedTokens.FirstOrDefault(t => t.Token == token);
            if (IsExist == null)
            {
                return Task.FromResult(false);

            }
            return Task.FromResult(true);
        }
    }
}
