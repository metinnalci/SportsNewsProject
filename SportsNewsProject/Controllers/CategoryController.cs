using Microsoft.AspNetCore.Mvc;
using SportsNewsProject.Models.ORM.Context;
using SportsNewsProject.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly SportsNewsContext _newscontext;

        public CategoryController(SportsNewsContext newscontext)
        {
            _newscontext = newscontext;
        }
        public IActionResult Index()
        {
            List<CategoryVM> categories = _newscontext.Categories.Select(q => new CategoryVM()
            {
                ID = q.ID,
                Name = q.CategoryName,
                Description = q.Description,
                UpperCategoryId = q.UpperCategoryID,
                Adddate = q.AddDate
            }).ToList();
            return View(categories);
        }
    }
}
