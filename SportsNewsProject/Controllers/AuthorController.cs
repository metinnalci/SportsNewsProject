using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsNewsProject.Models.ORM.Context;
using SportsNewsProject.Models.ORM.Entities;
using SportsNewsProject.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Controllers
{
    public class AuthorController : Controller
    {
        private readonly SportsNewsContext _newscontext;

        public AuthorController(SportsNewsContext context)
        {
            _newscontext = context;
        }
        public IActionResult Index()
        {
            List<AuthorVM> authors = _newscontext.Authors.Where(q => q.IsDeleted == false).Select(q => new AuthorVM()
            {
                ID = q.ID,
                Name = q.Name,
                Surname = q.SurName,
                EMail = q.EMail,
                Phone = q.Phone,
                AddDate = q.AddDate,
            }).ToList();

            return View(authors);
        }

        public IActionResult Add()
        {
            //ViewBag.Categories = _newscontext.Categories.ToList();
            AuthorVM model = new AuthorVM();
            model.categoryCheck = _newscontext.Categories.Select(q => new CategoryCheckVM()
            {

                categoryid = q.ID,
                IsChecked = false,
                Name = q.CategoryName

            }).ToArray();

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(AuthorVM model, int[] categoryid)
        {
            List<CategoryCheckVM> categoryCheckVMs = new List<CategoryCheckVM>();

            if (ModelState.IsValid)
            {
                Author author = new Author();
                author.ID = model.ID;
                author.Name = model.Name;
                author.SurName = model.Surname;
                author.EMail = model.EMail;
                author.Phone = model.Phone;
                author.AddDate = model.AddDate;
                
                _newscontext.Authors.Add(author);
                _newscontext.SaveChanges();

                int authorid = author.ID;

                model.Categories = _newscontext.Categories.ToList();
                int[] selectedcategories = _newscontext.AuthorCategories.Where(q => q.AuthorID == authorid).Select(q => q.CategoryID).ToArray();

                foreach (var item in model.Categories)
                {
                    CategoryCheckVM categoryCheck = new CategoryCheckVM();
                    categoryCheck.categoryid = item.ID;

                    foreach (var item2 in selectedcategories)
                    {
                        if(item2 == categoryCheck.categoryid)
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
                    categoryCheckVMs.Add(categoryCheck);
                }

                model.categoryCheck = categoryCheckVMs.ToArray();

                for (int i = 0; i < categoryid.Length; i++)
                {
                    AuthorCategory authorcategory = new AuthorCategory();
                    authorcategory.CategoryID = categoryid[i];
                    authorcategory.AuthorID = authorid;
                    _newscontext.AuthorCategories.Add(authorcategory);

                }
                _newscontext.SaveChanges();
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            Author author = _newscontext.Authors.FirstOrDefault(x => x.ID == id);
            List<CategoryCheckVM> categoryChecks = new List<CategoryCheckVM>();

            AuthorVM model = new AuthorVM();
            model.Name = author.Name;
            model.Surname = author.SurName;
            model.EMail = author.EMail;
            model.Phone = author.Phone;
            model.AddDate = author.AddDate;
            model.Categories = _newscontext.Categories.ToList();
            //model.categoryid = _newscontext.AuthorCategories.Where(q => q.AuthorID == id).Select(q => q.CategoryID).ToArray();
            int[] selectedCategories = _newscontext.AuthorCategories.Where(q => q.AuthorID == id).Select(q => q.CategoryID).ToArray();

            foreach (var item in model.Categories)
            {
                CategoryCheckVM categoryCheck = new CategoryCheckVM();
                categoryCheck.categoryid = item.ID;


                foreach (var item2 in selectedCategories)
                {
                    if(item2 == categoryCheck.categoryid)
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

            return View(model);
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
                author.AddDate = model.AddDate;

                _newscontext.SaveChanges();

                int authorid = author.ID;

                model.Categories = _newscontext.Categories.ToList();
                int[] selectedCategories = _newscontext.AuthorCategories.Where(q => q.AuthorID == authorid).Select(q => q.CategoryID).ToArray();

                foreach (var item in model.Categories)
                {
                    CategoryCheckVM categoryCheck = new CategoryCheckVM();

                    categoryCheck.categoryid = item.ID;

                    foreach (var item2 in selectedCategories)
                    {
                        if(item2 == categoryCheck.categoryid)
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

            return View(model);
        }



        [HttpPost]
        public IActionResult Delete(int id)
        {
            Author author = _newscontext.Authors.FirstOrDefault(x => x.ID == id);
            author.IsDeleted = true;

            _newscontext.SaveChanges();

            return Json("Author Deleted Successfully!");
        }


    }
}

