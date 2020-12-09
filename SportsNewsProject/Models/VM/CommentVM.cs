using SportsNewsProject.Models.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.VM
{
    public class CommentVM
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string NewsTitle { get; set; }
        public string Content { get; set; }
        public DateTime AddDate { get; set; }
        public List<User> Users { get; set; }
        public List<News> News { get; set; }

    }
}
