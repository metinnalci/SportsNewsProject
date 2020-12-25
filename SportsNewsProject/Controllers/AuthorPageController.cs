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
    public class AuthorPageController : MainSiteController
    {
        private readonly SportsNewsContext _newscontext;

        public AuthorPageController(SportsNewsContext newscontext, IMemoryCache memoryCache) : base(newscontext, memoryCache)
        {
            _newscontext = newscontext;
        }

        public IActionResult Author(int id)
        {
            AuthorPageVM author = new AuthorPageVM();
            author.author = _newscontext.Authors.Where(q => q.IsDeleted == false).Include(q => q.News.Where(q => q.IsDeleted == false).OrderByDescending(q => q.AddDate)).FirstOrDefault(x => x.ID == id);
            author.news = _newscontext.News.Where(q => q.IsDeleted == false).OrderBy(q => q.AddDate).Take(4).ToList();

            return View(author);
        }
    }
}
