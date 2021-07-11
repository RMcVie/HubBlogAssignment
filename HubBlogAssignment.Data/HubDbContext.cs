using HubBlogAssignment.Data.Entities;
using HubBlogAssignment.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubBlogAssignment.Data
{
    public class HubDbContext : DbContext
    {
        public HubDbContext(DbContextOptions<HubDbContext> opts) : base(opts) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
            .HasMany(p => p.Categories)
            .WithMany(p => p.Posts)
            .UsingEntity<Dictionary<string, object>>(
                "PostCategory",
                j => j
                    .HasOne<Category>()
                    .WithMany()
                    .HasForeignKey("CategoryId"),
                j => j
                    .HasOne<Post>()
                    .WithMany()
                    .HasForeignKey("PostId"));

            modelBuilder.Entity<Category>();

            modelBuilder.Entity<Comment>();

            modelBuilder.Entity<User>();
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
