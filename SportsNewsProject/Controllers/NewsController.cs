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
    public class NewsController : Controller
    {
        private readonly SportsNewsContext _newscontext;

        public NewsController(SportsNewsContext context)
        {
            _newscontext = context;
        }
        public IActionResult Index()
        {
            List<NewsVM> news = _newscontext.News.Where(q => q.IsDeleted == false).Select(q => new NewsVM()
            {
                ID = q.ID,
                Title = q.Title,
                SubTitle = q.SubTitle,
                Content = q.Content,
                AuthorCategoryID = q.AuthorCategoryId

            }).ToList();

            return View(news);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(NewsVM model)
        {
            News article = new News();
            article.Title = model.Title;
            article.SubTitle = model.SubTitle;
            article.AuthorCategoryId = model.AuthorCategoryID;
            article.Content = model.Content;

            _newscontext.News.Add(article);
            _newscontext.SaveChanges();

            return View();
        }

        public IActionResult Edit(int id)
        {
            News article = _newscontext.News.FirstOrDefault(q => q.ID == id);
            NewsVM model = new NewsVM();

            model.Content = article.Content;
            model.AuthorCategoryID = article.AuthorCategoryId;
            model.Title = article.Title;
            model.SubTitle = article.SubTitle;


            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(NewsVM model)
        {
            News editarticle = _newscontext.News.FirstOrDefault(q => q.ID == model.ID);

            if (ModelState.IsValid)
            {

                editarticle.AuthorCategoryId = model.AuthorCategoryID;
                editarticle.Content = model.Content;
                editarticle.Title = model.Title;
                editarticle.SubTitle = model.SubTitle;

                _newscontext.SaveChanges();
            }
            return View();
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            News deletednews = _newscontext.News.FirstOrDefault(x => x.ID == id);
            deletednews.IsDeleted = true;
            _newscontext.SaveChanges();

            return Json("Article Successfully Deleted!");
        }
    }
}
