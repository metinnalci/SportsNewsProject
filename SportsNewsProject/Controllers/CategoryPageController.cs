using Microsoft.AspNetCore.Authorization;
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
    [AllowAnonymous]
    public class CategoryPageController : MainSiteController
    {
        private readonly SportsNewsContext _newsContext;

        public CategoryPageController(SportsNewsContext newsContext, IMemoryCache memoryCache) : base(newsContext, memoryCache)
        {
            _newsContext = newsContext;
        }
        public IActionResult Category(int id)
        {
            CategoryPageVM page = new CategoryPageVM();
            page.category = _newsContext.Categories.Include(q => q.News.OrderByDescending(x => x.AddDate)).ThenInclude(News => News.Author).Where(q => q.IsDeleted == false).FirstOrDefault(x => x.ID == id);
            page.news = _newsContext.News.Where(q => q.IsDeleted == false).OrderByDescending(q => q.AddDate).Take(4).ToList();
            return View(page);
        }
    }
}
