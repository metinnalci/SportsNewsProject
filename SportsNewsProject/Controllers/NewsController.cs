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

        
    }
}
