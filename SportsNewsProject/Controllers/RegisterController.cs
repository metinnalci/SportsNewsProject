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
                string confirmcode = Guid.NewGuid().ToString();
                User user = new User();
                user.Name = model.Name;
                user.SurName = model.Surname;
                user.NickName = model.Username;
                user.BirthDate = model.BirthDate;
                user.EMail = model.EMail;
                user.Password = model.Password;
                user.ConfirmCode = confirmcode;
                user.IsActive = false;

                _newscontext.Users.Add(user);
                _newscontext.SaveChanges();

                //email gönderme kodu. EMail ile kullanıcıya 31. satırdaki confirmcode u yolla. 
                //http://localhost:5000/Register/Confirm/22336525112asd
            }
            else
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpGet("Register/Confirm/{confirmcode}")]
        public IActionResult Confirmcode(string confirmcode)
        {
            User user = _newscontext.Users.FirstOrDefault(q => q.ConfirmCode == confirmcode);

            if (user != null)
            {
                user.IsActive = true;
                _newscontext.SaveChanges();

                //kullanıcıyı login yap
            }
            return Redirect("/Home/Index");
        }
    }
}
