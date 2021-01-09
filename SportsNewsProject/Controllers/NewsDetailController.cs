using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SportsNewsProject.Models.Helpers;
using SportsNewsProject.Models.ORM.Context;
using SportsNewsProject.Models.ORM.Entities;
using SportsNewsProject.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Controllers
{
    [AllowAnonymous]
    public class NewsDetailController : MainSiteController
    {
        private readonly SportsNewsContext _newscontext;

        public NewsDetailController(SportsNewsContext newscontext, IMemoryCache memoryCache) : base(newscontext, memoryCache)
        {
            _newscontext = newscontext;
        }

        [Route("haber/{id}/{title}")]
        public IActionResult Index(NewsVM model)
        {
            NewsVM modell = new NewsVM();
            News news = new News();

            news = _newscontext.News.Include(q => q.Author).Include(q => q.CommentList).ThenInclude(q => q.User).FirstOrDefault(x => x.ID == model.ID);

            modell.ID = model.ID;
            modell.Title = news.Title;
            modell.SubTitle = news.SubTitle;
            modell.AuthorName = news.Author.Name;
            modell.Content = news.Content;
            modell.AddDate = news.AddDate;
            modell.Comments = news.CommentList.Where(q => q.IsDeleted == false).OrderByDescending(q => q.AddDate).ToList();
            modell.Categories = _newscontext.Categories.Where(q => q.ID == news.CategoryID).Include(q => q.News).ThenInclude(q => q.Author).OrderByDescending(q => q.AddDate).ToList();
            modell.authorid = news.AuthorID;

            return View(modell);
        }


        [HttpPost]
        public IActionResult AddComment(NewsVM model)
        {
            Comment comment = new Comment();
            User user = new User();
            News news = _newscontext.News.FirstOrDefault(x => x.ID == model.ID);
            model.Title = news.Title;
            
            comment.NewsId = model.ID;
            comment.Content = model.Comment.Content;
            user = _newscontext.Users.Where(q => q.EMail == model.UserEmail).FirstOrDefault();
            comment.UserId = user.ID;
            comment.ParentId = 0;

            _newscontext.Comments.Add(comment);
            _newscontext.SaveChanges();
            string url = "/haber/" + model.ID + "/" + UrlHelpers.FriendlyUrl(model.Title);

            return Redirect(url);
        }

        [HttpPost]
        public IActionResult Reply(ReplyVM replyVM, NewsVM model)
        {

            Comment comment = new Comment();
            User user = new User();
            user = _newscontext.Users.Where(q => q.EMail == replyVM.useremail).FirstOrDefault();
            News news = _newscontext.News.FirstOrDefault(x => x.ID == model.ID);
            model.Title = news.Title;

            comment.UserId = user.ID;
            comment.ParentId = replyVM.parentid;
            comment.NewsId = model.ID;
            comment.Content = replyVM.replycomment;

            _newscontext.Comments.Add(comment);
            _newscontext.SaveChanges();
            string url = "/haber/" + model.ID + "/" + UrlHelpers.FriendlyUrl(model.Title);

            return Redirect(url);
        }

        [HttpPost]
        public IActionResult Voting(int id, bool isTrue, string useremail)
        {
            Comment comment = new Comment();
            comment = _newscontext.Comments.FirstOrDefault(x => x.ID == id);
            

            if (comment.VoteEmail == null)
            {
                comment.VoteEmail = new List<string>() { };
                comment.VoteEmail?.Add(useremail);
                if (isTrue)
                {
                    comment.Likes++;
                }
                else
                {
                    comment.Dislikes++;
                }
            }
            else
            {
                int flag = comment.VoteEmail.IndexOf(useremail);
                if (flag == -1)
                {
                    comment.VoteEmail.Add(useremail);
                    if (isTrue)
                    {
                        comment.Likes++;
                    }
                    else
                    {
                        comment.Dislikes++;
                    }
                }
            }
            
            _newscontext.SaveChanges();

            return Json("Oyunuz başarıyla alınmıştır!");

        }

    }
}
