using IdentityModel;
using IdentityServer4.Validation;
using LpIdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace LpIdentityServer.Services
{
    public class IdentityResourceOwnerPasswValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var existuser = await _userManager.FindByEmailAsync(context.UserName);
            if (existuser is null)
            {
                return;
            }
            var passwordhcheck = await _userManager.CheckPasswordAsync(existuser, context.Password);
            if (passwordhcheck is false)  return;

            context.Result = new GrantValidationResult(existuser.Id, OidcConstants.AuthenticationMethods.Password);
        }
    }
}
