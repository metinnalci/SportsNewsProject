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
            charts.Categories = _newscontext.Categories.ToList();
            charts.Authors = _newscontext.Authors.ToList();
            charts.News = _newscontext.News.Include(q => q.AuthorCategory.Category).Include(q => q.AuthorCategory.Author).ToList();
            return View(charts);
        }
    }
}
