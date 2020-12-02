using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.ORM.Entities
{
    public class AuthorCategory
    {
        public int ID { get; set; }

        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public Category Category { get; set; }

        public int AuthorID { get; set; }

        [ForeignKey("AuthorID")]
        public Author Author { get; set; }
    }
}
