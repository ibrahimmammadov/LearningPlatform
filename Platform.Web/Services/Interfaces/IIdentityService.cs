using IdentityModel.Client;
using Platform.Web.Models;
using Shared.DTOs;

namespace Platform.Web.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<bool>> SignIn(SignInInput signInInput, CancellationToken cancellationToken);
        Task<TokenResponse> GetAccsessTokenByRefreshToken();
        Task RevokeRefreshToken();
    }
}
