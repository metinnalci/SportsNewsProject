using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.ORM.Entities
{
    public class AdminUser : BaseEntity
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public DateTime LastLoginDate { get; set; }
        
    }
}
