using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using SportsNewsProject.Models.ORM.Context;
using SportsNewsProject.Models.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Areas.Admin.Controllers
{
    
    [Area("Admin")]
    [Authorize(Policy = "AdminAccess")]
    public class BaseController : Controller
    {
        private readonly SportsNewsContext _newscontext;
        private readonly IMemoryCache _memoryCache;

        public BaseController(SportsNewsContext newscontext, IMemoryCache memoryCache)
        {
            _newscontext = newscontext;
            _memoryCache = memoryCache;
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.email = HttpContext.User.Claims.ToArray()[0].Value;
            List<AdminMenu> menu = new List<AdminMenu>();

            bool isExist = _memoryCache.TryGetValue("menus", out menu);

            if (!isExist)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60));

                 menu = _newscontext.AdminMenus.ToList();

                _memoryCache.Set("menus", menu, cacheEntryOptions);
            }

            ViewBag.menu = menu;
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

    }
}
