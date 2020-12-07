using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.VM
{
    public class UserVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }

        [Required(ErrorMessage ="Please fill required areas!")]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }

        [Required(ErrorMessage = "Please fill required areas!")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password!")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = ("Please enter the same password!"))]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime AddDate { get; set; }

    }
}
