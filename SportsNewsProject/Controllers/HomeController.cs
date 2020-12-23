using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SportsNewsProject.Models.ORM.Context;
using SportsNewsProject.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Controllers
{
    public class HomeController : MainSiteController
    {
        private readonly SportsNewsContext _newscontext;

        public HomeController(SportsNewsContext newscontext, IMemoryCache memoryCache) : base(newscontext, memoryCache)
        {
            _newscontext = newscontext;
        }

        public IActionResult Index()
        {
            HomeVM home = new HomeVM();
            home.Categories = _newscontext.Categories.Include(q => q.News).Where(q => q.IsDeleted == false && q.UpperCategoryID >= 1).ToList();
            home.News = _newscontext.News.Where(q => q.IsDeleted == false).Include(q => q.Author).OrderByDescending(q => q.AddDate).ToList();

            return View(home);
        }
    }
}
