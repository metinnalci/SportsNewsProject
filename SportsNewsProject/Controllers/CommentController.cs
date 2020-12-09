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
    public class CommentController : Controller
    {
        private readonly SportsNewsContext _newscontext;

        public CommentController(SportsNewsContext newscontext)
        {
            _newscontext = newscontext;
        }
        public IActionResult Index()
        {
            List<CommentVM> comments = _newscontext.Comments.Where(q => q.IsDeleted == false).Include(q => q.News).Include(q => q.User).Select(q => new CommentVM()
            {
                ID = q.ID,
                Username = q.User.NickName,
                NewsTitle = q.News.Title,
                Content = q.Content,
                AddDate = q.AddDate

            }).ToList();

            return View(comments);
        }

        public IActionResult Edit(int id)
        {
            CommentVM model = _newscontext.Comments.Select(q => new CommentVM()
            {
                ID = q.ID,
                Users = _newscontext.Users.ToList(),
                News = _newscontext.News.ToList(),
                Content = q.Content,

            }).FirstOrDefault(q => q.ID == id);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CommentVM model,int userid,int newsid)
        {
            Comment comments = _newscontext.Comments.FirstOrDefault(q => q.ID == model.ID);

            if (ModelState.IsValid)
            {
                comments.Content = model.Content;
                comments.NewsId = newsid;
                comments.UserId = userid;
            }

            _newscontext.SaveChanges();
            return RedirectToAction("Index", "Comment");
        }
        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Comment comment = _newscontext.Comments.FirstOrDefault(x => x.ID == id);

            comment.IsDeleted = true;

            _newscontext.SaveChanges();

            return Json("Comment Successfully Deleted!");
        }
    }
}
