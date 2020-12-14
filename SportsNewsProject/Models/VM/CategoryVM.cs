using SportsNewsProject.Models.ORM.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.VM
{
    public class CategoryVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please fill required areas!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please fill required areas!")]
        public string Description { get; set; }
        public int? UpperCategoryId { get; set; }
        public string UpperCategoryName { get; set; }
        public List<Category> UpperCategory { get; set; }
        public DateTime Adddate { get; set; }
        

    }
}