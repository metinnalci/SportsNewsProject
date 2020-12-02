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
    public class UserController : Controller
    {
        private readonly SportsNewsContext _newscontext;

        public UserController(SportsNewsContext newscontext)
        {
            _newscontext = newscontext;
        }
        public IActionResult Index()
        {
            List<UserVM> users = _newscontext.Users.Select(q => new UserVM()
            {
                ID = q.ID,
                Name = q.Name,
                SurName = q.SurName,
                NickName = q.NickName,
                AddDate = q.AddDate,
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
                user.AddDate = model.AddDate;
                user.IsDeleted = model.IsDeleted;

                _newscontext.Users.Add(user);
                _newscontext.SaveChanges();
            }
            
            return View();
        }
    }
}
