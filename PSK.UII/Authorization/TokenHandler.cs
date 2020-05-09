using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using PSK.Model.Services;
using System.Threading.Tasks;

namespace PSK.UI
{
    public class TokenHandler : AuthorizationHandler<TokenRequirement>
    {
        private readonly ITokenValidator _tokenValidator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenHandler(ITokenValidator tokenValidator, IHttpContextAccessor httpContextAccessor)
        {
            _tokenValidator = tokenValidator;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TokenRequirement requirement)
        {
            string authHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];

            if (authHeader != null && authHeader.StartsWith("Token"))
            {
                if (!_tokenValidator.Validate(authHeader.Substring("Token ".Length).Trim()))
                    _httpContextAccessor.HttpContext.Response.StatusCode = 401;
            }
            else
            {
                _httpContextAccessor.HttpContext.Response.StatusCode = 401;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
    public class TokenRequirement : IAuthorizationRequirement
    {
    }
}
