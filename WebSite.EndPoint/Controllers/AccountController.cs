using Application.Baskets;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebSite.EndPoint.Models.ViewModel.Register;
using WebSite.EndPoint.Models.ViewModel.User;
using WebSite.EndPoint.Utilities.Filters;

namespace WebSite.EndPoint.Controllers
{
    [ServiceFilter(typeof(SaveVisitorFilter))]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IBasketService _basketService;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IBasketService basketService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _basketService = basketService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            User newUser = new User
            {
                Email = register.Email,
                UserName = register.Email,
                FullName = register.FullName,
                PhoneNumber = register.PhoneNumber
            };

            var result = _userManager.CreateAsync(newUser, register.Password).Result;

            if (result.Succeeded)
            {
                var user = _userManager.FindByNameAsync(newUser.Email).Result;
                TransferBasketForUser(user.Id);
                _signInManager.SignInAsync(user, true).Wait();
                return RedirectToAction(nameof(Profile));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return View(register);
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Login(string returnUrl = "/")
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = _userManager.FindByNameAsync(login.Email).Result;

            if (user is null)
            {
                ModelState.AddModelError("", "کاربر یافت نشد");
                return View(login);
            }
            _signInManager.SignOutAsync();

            var result = _signInManager.PasswordSignInAsync(user, login.Password, login.IsPersistent, true).Result;
            if (result.Succeeded)
            {
                TransferBasketForUser(user.Id);
                return Redirect(login?.ReturnUrl ?? "/");
            }

            ModelState.AddModelError("", "ورود به حساب کاربری موفقیت آمیز نبود");
            return View(login);
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private void TransferBasketForUser(string userId)
        {
            string cookieName = "BasketId";
            if (Request.Cookies.ContainsKey(cookieName))
            {
                var anonymousId = Request.Cookies[cookieName];
                _basketService.TransferBasket(anonymousId, userId);
                Response.Cookies.Delete(cookieName);
            }
        }
    }
}
