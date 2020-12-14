using Microsoft.AspNetCore.Mvc;
using SportsNewsProject.Models.ORM.Context;
using SportsNewsProject.Models.ORM.Entities;
using SportsNewsProject.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly SportsNewsContext _newscontext;

        public UserController(SportsNewsContext newscontext)
        {
            _newscontext = newscontext;
        }
        public IActionResult Index()
        {
            List<UserVM> users = _newscontext.Users.Where(q => q.IsDeleted == false).Select(q => new UserVM()
            {
                ID = q.ID,
                Name = q.Name,
                SurName = q.SurName,
                NickName = q.NickName,
                BirthDate = q.BirthDate,
                EMail = q.EMail

            }).ToList();

            return View(users);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(UserVM model)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.Name = model.Name;
                user.SurName = model.SurName;
                user.EMail = model.EMail;
                user.NickName = model.NickName;
                user.Password = model.Password;
                user.BirthDate = model.BirthDate;

                _newscontext.Users.Add(user);
                _newscontext.SaveChanges();
            }
            else
            {
                return View();
            }

            return RedirectToAction("Index", "User");
        }

        public IActionResult Edit(int id)
        {
            UserVM model = _newscontext.Users.Select(q => new UserVM()
            {

                ID = q.ID,
                Name = q.Name,
                SurName = q.SurName,
                EMail = q.EMail,
                NickName = q.NickName,
                Password = q .Password,
                BirthDate = q.BirthDate,

            }).FirstOrDefault(x => x.ID == id);
            

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(UserVM model)
        {
            User user = _newscontext.Users.FirstOrDefault(x => x.ID == model.ID);

            if (ModelState.IsValid)
            {
                user.Name = model.Name;
                user.SurName = model.SurName;
                user.EMail = model.EMail;
                user.NickName = model.NickName;
                user.Password = model.Password;
                user.BirthDate = model.BirthDate;

                _newscontext.SaveChanges();
            }
            else
            {
                return View(model);
            }

            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            User user = _newscontext.Users.FirstOrDefault(x => x.ID == id);

            user.IsDeleted = true;

            _newscontext.SaveChanges();

            return Json("User Successfully Deleted!");
        }

    }
}
