using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.ORM.Entities
{
    public class Comment : BaseEntity
    {
        public int NewsId { get; set; }
        [ForeignKey("NewsId")]
        public News News { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public string Content { get; set; }

        public int? ParentId { get; set; }

        public int Likes { get; set; } = 0;
        public int Dislikes { get; set; } = 0;
    }
}
