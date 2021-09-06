using BackToWork.DAClasses;
using BackToWork.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BackToWork.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IDBDao _dba;
        public AccountController(ILogger<AccountController> logger, IDBDao dba)
        {
            _dba = dba;
        }
        
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                DbResultViewModel users = _dba.Users;
                foreach (object[] l in users.exemplars)
                    for (int i = 0; i < l.Length; i++)
                    {
                        if (model.Login == Convert.ToString(l[i]))
                        {
                            ModelState.AddModelError("", "Некорректные логин и(или) пароль"); return View(model);
                        }
                    }
                User user;
                object[] cont = new object[4];
                // добавляем пользователя в бд
                using (SHA256 my = SHA256.Create())
                {
                    user = new User (model.Login,  System.Text.Encoding.Default.GetString(my.ComputeHash(Encoding.ASCII.GetBytes(model.Password))),model.Role );
                    
                    cont[0] = model.Login;
                    cont[1] = System.Text.Encoding.Default.GetString(my.ComputeHash(Encoding.ASCII.GetBytes(model.Password)));
                    cont[2] = model.Role;
                    cont[3] = model.Email;
                }
                DAContext context = new DAContext();
                context.content = cont;
                _dba.InsertAsync("my_user", new string[0], context);
                await Authenticate(user); // аутентификация

                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Введены не корректные данные");
            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (ModelState.IsValid)
            {


                DbResultViewModel users = _dba.Users;
                User user = null;
                using (SHA256 my = SHA256.Create())
                {
                    
                    foreach (object[] l in users.exemplars)
                        for (int i = 0; i < l.Length; i++)
                        {
                            if (model.Login == Convert.ToString(l[0]) && Convert.ToString(l[1]) == System.Text.Encoding.Default.GetString(my.ComputeHash(Encoding.ASCII.GetBytes(model.Password))))
                            {
                                
                                user = new User(model.Login,model.Password,Convert.ToString(l[2]));
                            }
                        }
                }

                if (user != null)
                {
                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            ModelState.AddModelError("", "Введены не корректные данные");
            return View(model);
        }
        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }


    }
}
