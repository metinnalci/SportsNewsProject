using SportsNewsProject.Models.ORM.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.VM
{
    public class AuthorVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please fill the required area!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please fill the required area!")]
        public string Surname { get; set; }
        
        [Required(ErrorMessage = "Please fill the required area!")]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public DateTime AddDate { get; set; }
        public List<Category> Categories { get; set; }
    }
}
