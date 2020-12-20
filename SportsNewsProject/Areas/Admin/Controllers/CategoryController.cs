using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SportsNewsProject.Models.Attributes;
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
    public class CategoryController : BaseController
    {
        private readonly SportsNewsContext _newscontext;

        public CategoryController(SportsNewsContext newscontext, IMemoryCache memoryCache) : base(newscontext, memoryCache)
        {
            _newscontext = newscontext;
        }

        [RoleTest(EnumRoles.CategoryList)]
        public IActionResult Index()
        {
            List<CategoryVM> categories = _newscontext.Categories.Where(q => q.IsDeleted == false).Select(q => new CategoryVM()
            {
                ID = q.ID,
                Name = q.CategoryName,
                Description = q.Description,
                UpperCategoryId = q.UpperCategoryID,
                Adddate = q.AddDate
            }).OrderByDescending(q => q.Adddate).ToList();
            return View(categories);
        }

        [RoleTest(EnumRoles.CategoryAdd)]
        public IActionResult Add()
        {
            return View(GetCategoryVMForAdd());
        }

        [HttpPost]
        public IActionResult Add(CategoryVM model,int? uppercategoryid)
        {
            if (ModelState.IsValid)
            {
                if(uppercategoryid == 1)
                {
                    Category category = new Category();
                    category.CategoryName = model.Name;
                    category.Description = model.Description;
                    category.UpperCategoryID = 1;
                    _newscontext.Categories.Add(category);
                    _newscontext.SaveChanges();
                }
                else
                {
                    Category subcategory = new Category();
                    subcategory.UpperCategoryID = uppercategoryid;
                    subcategory.CategoryName = model.Name;
                    subcategory.Description = model.Description;
                    _newscontext.Categories.Add(subcategory);
                    _newscontext.SaveChanges();
                }
                
            }
            else
            {
                return View(GetCategoryVMForAdd());
            }
            return RedirectToAction("Index", "Category");
        }

        [RoleTest(EnumRoles.CategoryEdit)]
        public IActionResult Edit(int id)
        {
            return View(GetCategoryVMForEdit(id));
        }

        [HttpPost]
        public IActionResult Edit(CategoryVM model,int? uppercategoryid)
        {
            Category category = _newscontext.Categories.FirstOrDefault(x => x.ID == model.ID);

            if (ModelState.IsValid)
            {
                category.CategoryName = model.Name;
                category.Description = model.Description;
                if(uppercategoryid == 1)
                {
                    category.UpperCategoryID = 1;
                }
                else
                {
                    category.UpperCategoryID = uppercategoryid;
                }
                

                _newscontext.SaveChanges();
            }
            else
            {
                return View(GetCategoryVMForEdit(model.ID));
            }
            return RedirectToAction("Index", "Category");
        }

        [RoleControl(EnumRoles.CategoryDelete)]

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Category category = _newscontext.Categories.FirstOrDefault(x => x.ID == id);

            category.IsDeleted = true;

            _newscontext.SaveChanges();

            return Json("Category Successfully Deleted!");
        }


        CategoryVM GetCategoryVMForAdd()
        {
            CategoryVM model = new CategoryVM();
            model.UpperCategory = _newscontext.Categories.Where(q => q.UpperCategoryID == 0 || q.UpperCategoryID == 1 && q.IsDeleted == false).ToList();

            return model;
        }

        CategoryVM GetCategoryVMForEdit(int id)
        {
            CategoryVM model = _newscontext.Categories.Select(q => new CategoryVM()
            {
                ID = q.ID,
                Name = q.CategoryName,
                Description = q.Description,
                UpperCategoryId = q.UpperCategoryID,
                UpperCategory = _newscontext.Categories.Where(q => q.UpperCategoryID == 0 || q.UpperCategoryID == 1 && q.IsDeleted == false).ToList()

            }).FirstOrDefault(x => x.ID == id);

            return model;
        }


    }
}
