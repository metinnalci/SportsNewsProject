using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SportsNewsProject.Models.ORM.Context;
using SportsNewsProject.Models.ORM.Entities;
using SportsNewsProject.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Controllers
{
    public class NewsDetailController : MainSiteController
    {
        private readonly SportsNewsContext _newscontext;

        public NewsDetailController(SportsNewsContext newscontext, IMemoryCache memoryCache) : base(newscontext, memoryCache)
        {
            _newscontext = newscontext;
        }

        public IActionResult Index(int id, NewsVM? model)
        {
            NewsVM modell = new NewsVM();
            News news = new News();
            id = model.ID;
            news = _newscontext.News.Include(q => q.Author).Include(q => q.CommentList).ThenInclude(q => q.User).FirstOrDefault(x => x.ID == id);

            modell.ID = id;
            modell.Title = news.Title;
            modell.SubTitle = news.SubTitle;
            modell.AuthorName = news.Author.Name;
            modell.Content = news.Content;
            modell.AddDate = news.AddDate;
            modell.Comments = news.CommentList.Where(q => q.IsDeleted == false).OrderByDescending(q => q.AddDate).ToList();
            
            return View(modell);
        }


        [HttpPost]
        public IActionResult AddComment(NewsVM model)
        {
            Comment comment = new Comment();
            User user = new User();

            //int id = model.ID;
            comment.NewsId = model.ID;
            comment.Content = model.Comment.Content;
            user = _newscontext.Users.Where(q => q.EMail == model.Comment.User.EMail).FirstOrDefault();
            comment.UserId = user.ID;
            //comment.User.EMail = user.EMail;
            //comment.User.NickName = user.NickName;

            _newscontext.Comments.Add(comment);
            _newscontext.SaveChanges();
            
            return RedirectToAction("Index", "NewsDetail", model);
        }
    }
}
