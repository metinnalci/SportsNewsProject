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

namespace SportsNewsProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly SportsNewsContext _newscontext;

        public LoginController(SportsNewsContext newscontext)
        {
            _newscontext = newscontext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginVM model)
        {

            if (ModelState.IsValid)
            {
                AdminUser adminuser = _newscontext.AdminUsers.FirstOrDefault(x => x.EMail == model.EMail && x.Password == model.Password);
                if (adminuser != null)
                {

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.EMail),
                        new Claim(ClaimTypes.Role, adminuser.Role)

                    };

                    var userIdentity = new ClaimsIdentity(claims, "login");

                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.SignInAsync(principal);

                    adminuser.LastLoginDate = DateTime.Now;

                    _newscontext.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Kullanıcı adı veya şifre hatalı!";
                    return View();
                }
            }
            else
            {

                return View();
            }

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
