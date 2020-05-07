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
            if (!_tokenValidator.Validate(_httpContextAccessor.HttpContext.Request.Cookies["AuthToken"]))
                _httpContextAccessor.HttpContext.Response.StatusCode = 403;

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
    public class TokenRequirement : IAuthorizationRequirement
    {
    }
}
