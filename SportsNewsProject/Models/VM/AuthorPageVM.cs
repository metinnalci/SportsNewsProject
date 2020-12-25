using SportsNewsProject.Models.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.VM
{
    public class AuthorPageVM : MainBaseVM
    {
        public Author author { get; set; }
    }
}
