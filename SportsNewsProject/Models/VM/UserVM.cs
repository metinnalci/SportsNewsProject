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
        public string EMail { get; set; }

        [Required(ErrorMessage = "Please fill required areas!")]
        public string NickName { get; set; }
        public DateTime AddDate { get; set; }
        public bool IsDeleted { get; set; }

    }
}
