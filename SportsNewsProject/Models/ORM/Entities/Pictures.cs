using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.ORM.Entities
{
    public class Pictures : BaseEntity
    {
        public int NewsId { get; set; }
        [ForeignKey("NewsId")]
        public News News { get; set; }
        public string ImagePath { get; set; }
    }
}
