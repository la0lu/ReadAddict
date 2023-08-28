using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReadAddict.Data.Entities;
using ReadAddict.Services.Interfaces;
using ReadAddict.ViewModel;

namespace ReadAddict.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;

        public AccountController(IAccountService accountService, UserManager<AppUser> userManager, IEmailService emailService)
        {
            _accountService = accountService;
            _userManager = userManager;
            _emailService = emailService;
        }
        
        
        /*---------------------------Controller Actions-------------------*/


        [HttpGet]
        public IActionResult Register()
        {
            if (_accountService.IsLoggedInAsync(User))
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (_accountService.IsLoggedInAsync(User))
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                var result = await _accountService.RegisterAsync(model);
                if (result.Succeeded)
                    return RedirectToAction("NewUser", "Account");
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
            }
            return View(model);

        }


        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            if (_accountService.IsLoggedInAsync(User))
                return RedirectToAction("Index", "Home");

            ViewBag.ReturnUrl = ReturnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string ReturnUrl)
        {
            if (_accountService.IsLoggedInAsync(User))
                return RedirectToAction("Index", "Home");

            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user != null)
                {
                    if (await _accountService.LoginAsync(user, model.Password))
                    {
                        if (string.IsNullOrEmpty(ReturnUrl))
                            return RedirectToAction("Index", "Home");
                    }
                        
                }
                //ModelState.AddModelError("Login Failed", "Invalid Credentials");
                ViewBag.Err = "Invalid email or password";

            }
            
            return View(model);

        }

        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
                return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult NewUser()
        {
            if (_accountService.IsLoggedInAsync(User))
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }


        [HttpGet]
        public IActionResult ResetPassword(string Email, string Token)
        {
            var viewObj = new ResetPasswordViewModel();

            if (string.IsNullOrEmpty(Email))
            {
                ViewBag.ErrEmail = "Email is required";
            }
                  
            if(string.IsNullOrEmpty(Token))
            {
                ViewBag.ErrToken = "Token is required";
            }

            viewObj.Email = Email;
            viewObj.Token = Token;

            return View(viewObj);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if(user != null)
                    {
                        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    foreach(var err in result.Errors)
                    {
                        ModelState.AddModelError(err.Code, err.Description);
                    }
                }

                ViewBag.Err = "Email is invalid";
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            if (_accountService.IsLoggedInAsync(User))
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (_accountService.IsLoggedInAsync(User))
                return RedirectToAction("Index", "Home");

            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                    ModelState.AddModelError("", "Invalid email");
                else
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var link =  Url.Action("ResetPassword", "Account", new { model.Email, token }, Request.Scheme);

                    if (!string.IsNullOrEmpty(link))
                    {
                        if (await _emailService.SendEmailAsync(model.Email, "Reset Password Link", link))
                        {
                            ViewBag.Err = "A reset password link has been sent to the email provided. Please go to the email and click on the link to continue";
                        }
                        else
                        {
                            ViewBag.Err = "Failed to send a reset passeord link. Please try again.";
                        }
                    }

                   
                }
              
                
            }
            return View();
        }

    }
}
