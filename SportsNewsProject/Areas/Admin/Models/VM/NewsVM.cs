using Microsoft.AspNetCore.Http;
using SportsNewsProject.Models.ORM.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.VM
{
    public class NewsVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Article must have a title!")]
        public string Title { get; set; }
        public string SubTitle { get; set; }

        [Required(ErrorMessage = "You must give your article a content!")]
        public string Content { get; set; }
        public DateTime AddDate { get; set; }
        public string CategoryName { get; set; }
        public string AuthorName { get; set; }
        public List<IFormFile> articleimages { get; set; }
        public List<string> MainImagePath { get; set; }
        public List<Category> Categories { get; set; }
        public List<Author> Authors { get; set; }
        public List<Comment> Comments { get; set; }
        public Comment Comment { get; set; }
        
    }
}
