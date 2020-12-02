using Microsoft.EntityFrameworkCore;
using SportsNewsProject.Models.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Models.ORM.Context
{
    public class SportsNewsContext : DbContext
    {
        public SportsNewsContext(DbContextOptions<SportsNewsContext> options) : base(options)
        {

        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorCategory> AuthorCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Pictures> Pictures { get; set; }
        public DbSet<News> News { get; set; }
    }
}
