using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.VM
{
    public class MainRegisterVM
    {

        [Required(ErrorMessage = "İsim girilmesi zorunlu!")]
        public string Name { get; set; }
        public string Surname { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı zorunlu!")]
        public string Username { get; set; }

        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Email zorunlu!")]
        public string EMail { get; set; }

        [Required(ErrorMessage = "Şifre zorunlu!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifrenizi doğrulayın!")]
        [Compare("Password",ErrorMessage = ("Lütfen aynı şifreyi giriniz!"))]
        public string ConfirmPassword { get; set; }
    }
}
