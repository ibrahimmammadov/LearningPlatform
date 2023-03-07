using IdentityServer4.Validation;
using LpIdentityServer.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LpIdentityServer.Services
{
    public class TokenExchangeExtensionGrantValidator : IExtensionGrantValidator
    {
        public string GrantType => GrantTypesExtension.TokenExchangeClientCredentials.First();
        private readonly ITokenValidator _tokenValidator;

        public TokenExchangeExtensionGrantValidator(ITokenValidator tokenValidator)
        {
            _tokenValidator = tokenValidator;
        }

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var token = context.Request.Raw.Get("subject_token");
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new GrantValidationResult(IdentityServer4.Models.TokenRequestErrors.InvalidRequest, "token missing");
                return;
            }
            var tokenValidatorResult = await _tokenValidator.ValidateAccessTokenAsync(token);
            if (tokenValidatorResult.IsError)
            {
                context.Result = new GrantValidationResult(IdentityServer4.Models.TokenRequestErrors.InvalidGrant, "token invalid");
                return;
            }
            var subjectClaim = tokenValidatorResult.Claims.FirstOrDefault(x => x.Type == "sub");
            if (subjectClaim is null)
            {
                context.Result = new GrantValidationResult(IdentityServer4.Models.TokenRequestErrors.InvalidGrant, "token must contain sub value");
                return;
            }
            context.Result = new GrantValidationResult(subjectClaim.Value, "access_token",tokenValidatorResult.Claims);
            return;
        }
    }
}
