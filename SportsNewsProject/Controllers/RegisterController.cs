using Microsoft.AspNetCore.Mvc;
using SportsNewsProject.Models.ORM.Context;
using SportsNewsProject.Models.ORM.Entities;
using SportsNewsProject.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Controllers
{
    public class RegisterController : Controller
    {
        private readonly SportsNewsContext _newscontext;

        public RegisterController(SportsNewsContext newscontext)
        {
            _newscontext = newscontext;                
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(MainRegisterVM model)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.Name = model.Name;
                user.SurName = model.Surname;
                user.NickName = model.Username;
                user.BirthDate = model.BirthDate;
                user.EMail = model.EMail;
                user.Password = model.Password;

                _newscontext.Users.Add(user);
                _newscontext.SaveChanges();
            }
            else
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
