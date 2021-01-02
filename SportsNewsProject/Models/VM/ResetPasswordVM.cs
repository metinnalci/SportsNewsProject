using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.VM
{
    public class ResetPasswordVM
    {
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = ("Lütfen aynı şifreyi giriniz!"))]
        public string ConfirmPassword { get; set; }
    }
}
