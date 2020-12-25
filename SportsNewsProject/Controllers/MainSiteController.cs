using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using SportsNewsProject.Models.ORM.Context;
using SportsNewsProject.Models.ORM.Entities;
using SportsNewsProject.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Controllers
{
    public class MainSiteController : Controller
    {
        private readonly SportsNewsContext _newsContext;
        private readonly IMemoryCache _memoryCache;

        public MainSiteController(SportsNewsContext newsContext, IMemoryCache memoryCache)
        {
            _newsContext = newsContext;
            _memoryCache = memoryCache;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            List<Category> categories = new List<Category>();


            bool isExist = _memoryCache.TryGetValue("categories", out categories);

            if (!isExist)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60));

                categories = _newsContext.Categories.Where(q => q.IsDeleted == false).ToList();

                _memoryCache.Set("categories", categories, cacheEntryOptions);
            }

            ViewBag.categories = categories.Where(q => q.UpperCategoryID == 1);
            ViewBag.subcategories = categories.Where(q => q.UpperCategoryID > 1);

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}
