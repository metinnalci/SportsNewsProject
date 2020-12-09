using SportsNewsProject.Models.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.VM
{
    public class NewsVM
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Content { get; set; }
        public DateTime AddDate { get; set; }
        public string CategoryName { get; set; }
        public string AuthorName { get; set; }
        public List<Category> Categories { get; set; }
        public List<Author> Authors { get; set; }

    }
}
