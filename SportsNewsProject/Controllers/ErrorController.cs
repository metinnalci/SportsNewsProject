using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SportsNewsProject.Models.ORM.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Controllers
{
    [AllowAnonymous]
    public class ErrorController : MainSiteController
    {
        private readonly SportsNewsContext _newsContext;
        private readonly IMemoryCache _memoryCache;

        public ErrorController(SportsNewsContext newsContext, IMemoryCache memoryCache) : base(newsContext, memoryCache)
        {
            _newsContext = newsContext;
            _memoryCache = memoryCache;
        }

        [Route("error/{statusCode}")]
        public IActionResult PageNotFound(int statusCode)
        {
            if(statusCode == 404)
            {
                ViewBag.ErrorMessage = "Üzgünüz, Sayfa Bulunamadı...";
            }
            
            return View();
        }
    }
}
