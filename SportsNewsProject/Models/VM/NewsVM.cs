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
        public int AuthorCategoryID { get; set; }
        public DateTime AddDate { get; set; }

    }
}
