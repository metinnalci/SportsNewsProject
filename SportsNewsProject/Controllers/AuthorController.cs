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
            ViewBag.Categories = _newscontext.Categories.ToList();
            return View();
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

                _newscontext.Authors.Add(author);
                _newscontext.SaveChanges();

                int authorid = author.ID;

                ViewBag.Categories = _newscontext.Categories.ToList();

                for (int i = 0; i < categoryid.Length; i++)
                {
                    AuthorCategory authorcategory = new AuthorCategory();
                    authorcategory.CategoryID = categoryid[i];
                    authorcategory.AuthorID = authorid;
                    _newscontext.AuthorCategories.Add(authorcategory);

                }
                _newscontext.SaveChanges();


            }

            return View();
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
