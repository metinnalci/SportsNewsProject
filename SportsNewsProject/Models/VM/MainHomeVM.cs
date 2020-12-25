using SportsNewsProject.Models.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.VM
{
    public class MainHomeVM
    {
        public List<News> News { get; set; }
        public List<Category> Categories { get; set; }
        public News LastNews { get; set; }
    }
}
