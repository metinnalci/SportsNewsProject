using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.ORM.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public Nullable<int> UpperCategoryID { get; set; }
        public List<AuthorCategory> AuthorCategories { get; set; }
        public List<News> News { get; set; }
    }
}
