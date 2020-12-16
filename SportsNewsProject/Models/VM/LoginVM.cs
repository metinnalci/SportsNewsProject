using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.VM
{
    public class LoginVM
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        [Required(ErrorMessage ="Email boş geçilemez!")]
        public string EMail { get; set; }
        [Required(ErrorMessage ="Şifre boş geçilemez")]
        [Display(Name ="Şifre")]
        public string Password { get; set; }
    }
}
