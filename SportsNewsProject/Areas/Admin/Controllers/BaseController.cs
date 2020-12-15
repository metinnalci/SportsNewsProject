using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using SportsNewsProject.Models.ORM.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BaseController : Controller
    {
        private readonly SportsNewsContext _newscontext;
        private readonly IMemoryCache _memoryCache;

        public BaseController(SportsNewsContext context)
        {
            _newscontext = context;
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
             
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

    }
}
