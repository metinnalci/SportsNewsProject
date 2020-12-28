using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SportsNewsProject.Models.ORM.Context;
using SportsNewsProject.Models.ORM.Entities;
using SportsNewsProject.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SportsNewsProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly SportsNewsContext _newscontext;

        public LoginController(SportsNewsContext newscontext)
        {
            _newscontext = newscontext;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(MainLoginVM model)
        {
            if (ModelState.IsValid)
            {
                User user = _newscontext.Users.Where(q => q.IsDeleted == false).FirstOrDefault(x => x.EMail == model.EMail && x.Password == model.Password);
                if(user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, model.EMail),
                        new Claim(ClaimTypes.Name, user.Name)
                    };

                    var userIdentity = new ClaimsIdentity(claims, "login");
                    
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    
                    await HttpContext.SignInAsync(principal);

                    user.LastLogin = DateTime.Now;

                    _newscontext.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Email veya şifre hatalı";
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}
