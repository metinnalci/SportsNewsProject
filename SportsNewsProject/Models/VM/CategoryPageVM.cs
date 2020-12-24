using SportsNewsProject.Models.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.VM
{
    public class CategoryPageVM
    {
        public Category category { get; set; }
        public List<News> news { get; set; }
    }
}
