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
    public class HomeController : Controller
    {
        private readonly SportsNewsContext _newscontext;

        public HomeController(SportsNewsContext newscontext)
        {
            _newscontext = newscontext;
        }
        public IActionResult Index()
        {
            HomeVM charts = new HomeVM();
            charts.Categories = _newscontext.Categories.Take(5).ToList();
            charts.Authors = _newscontext.Authors.Where(q => q.IsDeleted == false).Take(5).ToList();
            charts.News = _newscontext.News.Where(q => q.IsDeleted == false).Take(5).ToList();
            charts.Users = _newscontext.Users.Where(q => q.IsDeleted == false).Take(5).ToList();

            ViewBag.TotalUser = _newscontext.Users.Count();
            ViewBag.TotalAuthor = _newscontext.Authors.Count();
            ViewBag.TotalArticle = _newscontext.News.Count();
            return View(charts);
        }
    }
}
