using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.Attributes
{
    public class RoleTest :Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (true)
            {
                context.Result = new RedirectToActionResult("Hata", "Error",null);
               
            }
        }
    }
}
