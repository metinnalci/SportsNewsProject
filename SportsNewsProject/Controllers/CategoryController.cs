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
    public class CategoryController : Controller
    {
        private readonly SportsNewsContext _newscontext;

        public CategoryController(SportsNewsContext newscontext)
        {
            _newscontext = newscontext;
        }
        public IActionResult Index()
        {
            List<CategoryVM> categories = _newscontext.Categories.Where(q => q.IsDeleted == false).Select(q => new CategoryVM()
            {
                ID = q.ID,
                Name = q.CategoryName,
                Description = q.Description,
                UpperCategoryId = q.UpperCategoryID,
                Adddate = q.AddDate
            }).ToList();
            return View(categories);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CategoryVM model)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category();
                category.CategoryName = model.Name;
                category.Description = model.Description;
                category.UpperCategoryID = model.UpperCategoryId;
                _newscontext.Categories.Add(category);
                _newscontext.SaveChanges();
            }
            else
            {
                return View();
            }
            return RedirectToAction("Index", "Category");
        }

        public IActionResult Edit(int id)
        {
            CategoryVM model = _newscontext.Categories.Select(q => new CategoryVM()
            {
                ID = q.ID,
                Name = q.CategoryName,
                Description = q.Description,
                UpperCategoryId = q.UpperCategoryID,

            }).FirstOrDefault(x => x.ID == id);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CategoryVM model)
        {
            Category category = _newscontext.Categories.FirstOrDefault(x => x.ID == model.ID);

            if (ModelState.IsValid)
            {
                category.CategoryName = model.Name;
                category.Description = model.Description;
                category.UpperCategoryID = model.UpperCategoryId;

                _newscontext.SaveChanges();
            }
            else
            {
                model.ID = category.ID;
                model.Name = category.CategoryName;
                model.Description = category.Description;
                model.UpperCategoryId = category.UpperCategoryID;

                return View(model);
            }
            return RedirectToAction("Index", "Category");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Category category = _newscontext.Categories.FirstOrDefault(x => x.ID == id);

            category.IsDeleted = true;

            _newscontext.SaveChanges();

            return Json("Category Successfully Deleted!");
        }


    }
}
