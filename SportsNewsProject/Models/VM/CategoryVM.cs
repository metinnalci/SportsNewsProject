using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.VM
{
    public class CategoryVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? UpperCategoryId { get; set; }
        public DateTime Adddate { get; set; }

    }
}
