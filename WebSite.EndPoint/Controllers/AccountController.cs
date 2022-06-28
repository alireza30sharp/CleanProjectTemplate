using Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebSite.EndPoint.Models.ViewModels.User;

namespace WebSite.EndPoint.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManger;
        private readonly SignInManager<User> _signInManager;    
        public AccountController(UserManager<User> userManger, SignInManager<User> signInManager)
        {
            _userManger = userManger;
            _signInManager = signInManager;
        }

   


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public  IActionResult Register(RegisterViewModel registerViewModel )
        {
           if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }
            User newUser = new User()
            {
                Email = registerViewModel.Email,
                FullName = registerViewModel.FullName,
                PhoneNumber = registerViewModel.PhoneNumber,
                UserName= registerViewModel.Email,

            };

            var Result=_userManger.CreateAsync(newUser,registerViewModel.Password).Result;
            if (Result.Succeeded)
            {
                return View(nameof(Profile));
            }


            foreach (var item in Result.Errors)
            {
                ModelState.AddModelError(item.Code, item.Description);

            }
            return View(registerViewModel);
        }


        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Login(string Url="/")
        {
            return View(new LoginViewModel
            {
                ReturnUrl = Url
            }) ;
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = _userManger.FindByNameAsync(login.Email).Result;
            if (user==null)
            {
                ModelState.AddModelError("", "کاربری یافت نشد");
                return View(login);
            }
            _signInManager.SignOutAsync();
            var signIn = _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe,true).Result;

            if (signIn.Succeeded)
            {
                return Redirect(login.ReturnUrl);
            }
            if (signIn.RequiresTwoFactor)
            {
                //لاگین دو مرحله ای

            }

            return View(login);
        }


        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
