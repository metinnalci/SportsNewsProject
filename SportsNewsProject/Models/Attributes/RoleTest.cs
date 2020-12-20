using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SportsNewsProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.Attributes
{
    public class RoleTest : Attribute, IAuthorizationFilter
    {
        string rol = "0";
        public RoleTest(EnumRoles roles)
        {
            rol = Convert.ToInt32(roles).ToString(); 
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string role = context.HttpContext.User.Claims.ToArray()[1].Value;

            if(role != null)
            {
                string[] adminrooles = role.Split(";");
                bool haveAccess = false;

                foreach (var item in adminrooles)
                {
                    if(item == rol)
                    {
                        haveAccess = true;
                    }
                }
                if (!haveAccess)
                {
                    context.Result = new RedirectToActionResult("YetkisizErisim", "Error", null);
                }
               
            }
            else 
            {
            context.Result = new RedirectToActionResult("YetkisizErisim", "Error", null);
            }

        }
    }
}
