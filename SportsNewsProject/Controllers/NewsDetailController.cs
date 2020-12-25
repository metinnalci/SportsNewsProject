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

        public IActionResult Index(int id)
        {
            NewsVM model = new NewsVM();
            News news = new News();
            news = _newscontext.News.Include(q => q.Author).Include(q => q.CommentList).ThenInclude(q => q.User).FirstOrDefault(x => x.ID == id);

            model.Title = news.Title;
            model.SubTitle = news.SubTitle;
            model.AuthorName = news.Author.Name;
            model.Content = news.Content;
            model.AddDate = news.AddDate;
            model.Comments = news.CommentList.Where(q => q.IsDeleted == false).OrderByDescending(q => q.AddDate).ToList();
            
            return View(model);
        }

        [HttpPost]
        public IActionResult AddComment(NewsVM model)
        {
            Comment comment = new Comment();
            comment.Content = model.Comment.Content;
            comment.NewsId = model.ID;
            comment.UserId = model.Comment.User.ID;
            comment.User.EMail = model.Comment.User.EMail;
            comment.User.NickName = model.Comment.User.NickName;

            _newscontext.Comments.Add(comment);
            _newscontext.SaveChanges();
            
            return RedirectToAction("NewsDetail","Index");
        }
    }
}
