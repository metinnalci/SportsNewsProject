using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsNewsProject.Models.ORM.Context;
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
                UserID = q.UserId,
                NewsID = q.NewsId,
                Content = q.Content,
                AddDate = q.AddDate

            }).ToList();
            return View(comments);
        }


    }
}
