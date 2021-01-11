using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.ORM.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public string SurName{ get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public List<News> News { get; set; }
        public List<AuthorCategory> AuthorCategories { get; set; }

    }
}
