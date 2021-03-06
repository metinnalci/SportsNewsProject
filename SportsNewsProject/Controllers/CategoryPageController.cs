﻿using Microsoft.AspNetCore.Authorization;
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

        [Route("kategori/{id}/{title}")]
        public IActionResult Category(int id)
        {
            CategoryPageVM page = new CategoryPageVM();
            page.category = _newsContext.Categories.Include(q => q.News.OrderByDescending(x => x.AddDate)).ThenInclude(News => News.Author).FirstOrDefault(x => x.ID == id && x.IsDeleted == false);
            page.news = _newsContext.News.Where(q => q.IsDeleted == false).OrderByDescending(q => q.AddDate).Take(4).ToList();
            return View(page);
        }

        [Route("altkategori/{id}/{title}")]
        public IActionResult SubCategory(int id)
        {
            CategoryPageVM subpage = new CategoryPageVM();
            subpage.category = _newsContext.Categories.Include(q => q.News.OrderByDescending(x => x.AddDate)).ThenInclude(News => News.Author).FirstOrDefault(x => x.ID == id && x.IsDeleted == false);
            subpage.news = _newsContext.News.Where(q => q.IsDeleted == false).OrderByDescending(q => q.AddDate).Take(4).ToList();
            return View(subpage);
        }
    }
}
