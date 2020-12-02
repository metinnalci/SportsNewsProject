﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.ORM.Entities
{
    public class News : BaseEntity
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Content { get; set; }
        public List<Pictures> PictureList { get; set; }
        public List<Comment> CommentList { get; set; }

        public int AuthorCategoryId { get; set; }
        [ForeignKey("AuthorCategoryId")]
        public AuthorCategory AuthorCategory { get; set; }

    }
}
