using Microsoft.AspNetCore.Identity;
using ReadAddict.Data.Entities;
using ReadAddict.Services.Interfaces;
using ReadAddict.ViewModel;
using System.Security.Claims;

namespace ReadAddict.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        public AccountService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
        {
            var newUser = new AppUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(newUser, model.PassWord);
            return result;
   
        }

        public async Task<bool> LoginAsync(AppUser user, string password)
        {
           var loginResult = await _signInManager.PasswordSignInAsync(user, password, false, false);

            if(loginResult.Succeeded)
                return true;
            return false;
        }

        public async Task LogoutAsync()
        {
           await _signInManager.SignOutAsync();
        }

        public bool IsLoggedInAsync(ClaimsPrincipal user)
        {
           if(_signInManager.IsSignedIn(user))
                return true;
           return false;
        }


    }
}
