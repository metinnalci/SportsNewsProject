using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SportsNewsProject.Models.ORM.Context;
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
            return View();
        }
    }
}
