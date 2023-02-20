using Platform.Web.Models;
using Platform.Web.Services.Interfaces;

namespace Platform.Web.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserViewModel> GetUser()
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _httpClient.GetFromJsonAsync<UserViewModel>("/api/user/getuser");
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
