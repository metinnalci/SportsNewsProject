using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SportsNewsProject.Models.Enums;
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

    public class AdminUserController : BaseController
    {
        private readonly SportsNewsContext _newscontext;

        public AdminUserController(SportsNewsContext newscontext, IMemoryCache memoryCache) : base(newscontext, memoryCache)
        {
            _newscontext = newscontext;
        }
        public IActionResult Index()
        {
            List<AdminUserVM> model = _newscontext.AdminUsers.Select(q => new AdminUserVM()
            {
                ID = q.ID,
                Name = q.Name,
                Surname = q.SurName,
                EMail = q.EMail,
                Roles = q.Role
            }).ToList();

            return View(model);
        }

        public IActionResult Add()
        {
            //Enum.GetValues(typeof(SomeEnum)).Cast<SomeEnum>().Cast<int>().ToList()
            return View();
        }
    }
}
