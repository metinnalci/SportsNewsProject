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
            }).ToList();

            return View(model);
        }

        public IActionResult Add()
        {
            List<RolesVM> roles = new List<RolesVM>();
            EnumRoles enumRoles = new EnumRoles();
            string rolid = Convert.ToInt32(enumRoles).ToString();
            string rolname = Convert.ToString(enumRoles);

            List<int> rolidd = Enum.GetValues(typeof(EnumRoles)).Cast<EnumRoles>().Cast<int>().ToList();
            //string[] rolnamee = Enum.GetNames(typeof(EnumRoles));

            AdminUserVM model = new AdminUserVM();
            
            return View(model);
        }
    }
}
//model.Roles.RoleId = Enum.GetValues(typeof(EnumRoles)).Cast<EnumRoles>().Cast<int>().ToArray();
//model.Roles.Ischecked = false;
//model.Roles.EnumRoles = Enum.GetNames(typeof(EnumRoles));