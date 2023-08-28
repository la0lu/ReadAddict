using Microsoft.AspNetCore.Identity;
using ReadAddict.Data.Entities;
using ReadAddict.ViewModel;
using System.Security.Claims;

namespace ReadAddict.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<IdentityResult> RegisterAsync(RegisterViewModel model);
        public Task<bool> LoginAsync(AppUser user, string password);

        public Task LogoutAsync();

        public bool IsLoggedInAsync(ClaimsPrincipal user);
    }
}
