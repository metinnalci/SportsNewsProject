using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class AuthorController : BaseController
    {
        private readonly SportsNewsContext _newscontext;

        public AuthorController(SportsNewsContext newscontext, IMemoryCache memoryCache) : base(newscontext, memoryCache)
        {
            _newscontext = newscontext;
        }

        [RoleTest(EnumRoles.AuthorList)]
        public IActionResult Index(List<AuthorVM> deneme)
        {
            if (deneme != null)
            {
                List<AuthorVM> model = _newscontext.Authors.Where(q => q.IsDeleted == false).Include(q => q.AuthorCategories).Select(q => new AuthorVM()
                {
                    ID = q.ID,
                    Name = q.Name,
                    Surname = q.SurName,
                    EMail = q.EMail,
                    Phone = q.Phone,
                    BirthDate = q.BirthDate,
                    Categories = q.AuthorCategories.Select(q => q.Category).ToList()

                }).OrderByDescending(q => q.ID).ToList();

                return View(model);
            }

            return View();
        }

        [RoleTest(EnumRoles.AuthorAdd)]
        public IActionResult Add()
        {
            return View(GetAuthorForAdd());
        }

        [HttpPost]
        public IActionResult Add(AuthorVM model, int[] categoryid)
        {
            if (ModelState.IsValid)
            {
                Author author = new Author();
                author.ID = model.ID;
                author.Name = model.Name;
                author.SurName = model.Surname;
                author.EMail = model.EMail;
                author.Phone = model.Phone;
                author.BirthDate = model.BirthDate;

                _newscontext.Authors.Add(author);
                _newscontext.SaveChanges();

                int authorid = author.ID;

                model.Categories = _newscontext.Categories.ToList();

                for (int i = 0; i < categoryid.Length; i++)
                {
                    AuthorCategory authorcategory = new AuthorCategory();
                    authorcategory.CategoryID = categoryid[i];
                    authorcategory.AuthorID = authorid;
                    _newscontext.AuthorCategories.Add(authorcategory);

                }
                _newscontext.SaveChanges();
            }
            else
            {
                return View(GetAuthorForAdd());
            }

            return RedirectToAction("Index", "Author");
        }

        [RoleTest(EnumRoles.AuthorEdit)]
        public IActionResult Edit(int id)
        {
            return View(GetAuthorForEdit(id));
        }

        [HttpPost]
        public IActionResult Edit(AuthorVM model, int[] categoryid)
        {
            Author author = _newscontext.Authors.FirstOrDefault(x => x.ID == model.ID);
            List<CategoryCheckVM> categoryChecks = new List<CategoryCheckVM>();

            if (ModelState.IsValid)
            {
                author.Name = model.Name;
                author.SurName = model.Surname;
                author.EMail = model.EMail;
                author.Phone = model.Phone;
                author.BirthDate = model.BirthDate;

                _newscontext.SaveChanges();

                int authorid = author.ID;

                model.Categories = _newscontext.Categories.Where(q => q.IsDeleted == false).ToList();
                int[] selectedCategories = _newscontext.AuthorCategories.Where(q => q.AuthorID == authorid).Select(q => q.CategoryID).ToArray();

                foreach (var item in model.Categories)
                {
                    CategoryCheckVM categoryCheck = new CategoryCheckVM();

                    categoryCheck.categoryid = item.ID;

                    foreach (var item2 in selectedCategories)
                    {
                        if (item2 == categoryCheck.categoryid)
                        {
                            categoryCheck.IsChecked = true;
                            break;
                        }
                        else
                        {
                            categoryCheck.IsChecked = false;
                        }
                    }

                    categoryCheck.Name = item.CategoryName;
                    categoryChecks.Add(categoryCheck);
                }

                model.categoryCheck = categoryChecks.ToArray();



                foreach (var item in categoryid)
                {
                    if (!selectedCategories.Contains(item))
                    {
                        AuthorCategory authorCategory = new AuthorCategory();
                        authorCategory.CategoryID = item;
                        authorCategory.AuthorID = authorid;
                        _newscontext.AuthorCategories.Add(authorCategory);
                    }
                }
                _newscontext.SaveChanges();
            }

            else
            {
                return View(GetAuthorForEdit(model.ID));
            }


            return RedirectToAction("Index", "Author");
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            Author author = _newscontext.Authors.FirstOrDefault(x => x.ID == id);
            author.IsDeleted = true;

            _newscontext.SaveChanges();

            return Json("Author Deleted Successfully!");
        }

        public IActionResult Detail(int id)
        {

            Author author = _newscontext.Authors.Include(q => q.AuthorCategories).ThenInclude(q => q.Category).FirstOrDefault(x => x.ID == id);

            return Json(author);


        }

        AuthorVM GetAuthorForAdd()
        {
            AuthorVM model = new AuthorVM();
            model.categoryCheck = _newscontext.Categories.Where(q => q.IsDeleted == false).Select(q => new CategoryCheckVM()
            {

                categoryid = q.ID,
                IsChecked = false,
                Name = q.CategoryName

            }).ToArray();

            return model;
        }

        AuthorVM GetAuthorForEdit(int id)
        {
            Author author = _newscontext.Authors.FirstOrDefault(x => x.ID == id);
            List<CategoryCheckVM> categoryChecks = new List<CategoryCheckVM>();

            AuthorVM model = new AuthorVM();
            model.Name = author.Name;
            model.Surname = author.SurName;
            model.EMail = author.EMail;
            model.Phone = author.Phone;
            model.BirthDate = author.BirthDate;
            model.Categories = _newscontext.Categories.Where(q => q.IsDeleted == false).ToList();
            int[] selectedCategories = _newscontext.AuthorCategories.Where(q => q.AuthorID == id).Select(q => q.CategoryID).ToArray();

            foreach (var item in model.Categories)
            {
                CategoryCheckVM categoryCheck = new CategoryCheckVM();
                categoryCheck.categoryid = item.ID;


                foreach (var item2 in selectedCategories)
                {
                    if (item2 == categoryCheck.categoryid)
                    {
                        categoryCheck.IsChecked = true;
                        break;
                    }
                    else
                    {
                        categoryCheck.IsChecked = false;
                    }

                }

                categoryCheck.Name = item.CategoryName;

                categoryChecks.Add(categoryCheck);
            }

            model.categoryCheck = categoryChecks.ToArray();

            return model;
        }
    }
}


