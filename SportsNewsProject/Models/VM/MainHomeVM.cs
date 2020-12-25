using SportsNewsProject.Models.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.VM
{
    public class MainHomeVM : MainBaseVM
    {
        public List<Category> Categories { get; set; }
    }
}
