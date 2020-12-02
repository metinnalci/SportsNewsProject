using Microsoft.AspNetCore.Mvc;
using SportsNewsProject.Models.ORM.Context;
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
    }
}
