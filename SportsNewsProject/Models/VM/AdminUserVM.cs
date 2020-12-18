using SportsNewsProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.VM
{
    public class AdminUserVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "EMail alanı boş geçilemez")]
        public string EMail { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        [Required(ErrorMessage = "Şifre alanı boş geçilemez")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre tekrar alanı boş geçilemez")]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]
        [Display(Name = "Şifre tekrar")]
        public string Confirmpassword { get; set; }
        public string Roles { get; set; }
        public string[] EnumRoles { get; set; }

    }
}
