using Microsoft.AspNetCore.Mvc.Filters;
using SportsNewsProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.Attributes
{
    public class RoleControl : ActionFilterAttribute
    {
        string pagerol = "0";

        public RoleControl(EnumRoles enumRoles)
        {
            pagerol = Convert.ToInt32(enumRoles).ToString();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            string roles = context.HttpContext.User.Claims.ToArray()[1].Value;

            if (roles != null)
            {
                string[] userroles = roles.Split(';');
                bool isaccaccessible = false;

                foreach (var item in userroles)
                {
                    if (item == pagerol)
                    {
                        isaccaccessible = true;
                    }
                }
                if (isaccaccessible)
                {
                    base.OnActionExecuting(context);
                }
                else
                {
                        context.HttpContext.Response.Redirect("/Admin/Error/YetkisizErisim");
                }
            }
            else
            {
                context.HttpContext.Response.Redirect("/Admin/Error/YetkisizErisim");
            }


        }


        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}
