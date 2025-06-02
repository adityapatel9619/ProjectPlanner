using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProjectPlanner.IMethod;
using ProjectPlanner.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ProjectPlanner.Common;

namespace ProjectPlanner.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccount _account;

        public AccountController(IAccount account)
        {
            _account = account;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegistrationViewModel registration)
        {
            if (ModelState.IsValid)
            {
                string Message = "";
                try
                {
                    int value =  _account.SaveNewEntry(registration);

                    if (value ==1)
                    {
                        Message = $"{registration.Email} account Created !!";
                        ViewBag.Message = Message;
                    }
                    else
                    {
                        ViewBag.Error = $"Something went wrong. Please try again !!!";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = $"{ex.Message.ToString()}";
                    //ModelState.AddModelError("", ex.Message.ToString());
                    ModelState.Clear();
                }
            }
            return View(registration);
        }

        public IActionResult Login()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            if (ViewBag.Name != null)
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Name = HttpContext.User.Identity.Name;
                if (ViewBag.Name != null)
                {
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    clsGlobal global = new clsGlobal();
                    var user = _account.GetRegistrations().Where(x => (x.UserName == model.UserNameOrEmail || x.Email == model.UserNameOrEmail) && global.Decrypt(x.Password) == model.Password).FirstOrDefault();

                    if (user != null)
                    {
                        //Valid user

                        var claim = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.Email),
                            new Claim("Name",user.FirstName),
                        };

                        var claimsIdentity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                        return RedirectToAction("Dashboard");
                    }
                    else
                    {
                        ViewBag.Error = "Username/Email or Password is not correct. Please try again !!!";
                    }
                }
            }

            return View(model);
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }
    }
}
