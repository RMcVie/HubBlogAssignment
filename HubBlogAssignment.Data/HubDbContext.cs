using HubBlogAssignment.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using HubBlogAssignment.Data.Entities.Database;

namespace HubBlogAssignment.Data
{
    public class HubDbContext : DbContext
    {
        public HubDbContext(DbContextOptions<HubDbContext> opts) : base(opts) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostDb>()
                .ToTable("Post")
                .HasMany(p => p.Categories)
                .WithMany(p => p.Posts)
                .UsingEntity<Dictionary<string, object>>(
                    "PostCategory",
                    j => j
                        .HasOne<Category>()
                        .WithMany()
                        .HasForeignKey("CategoryId"),
                    j => j
                        .HasOne<PostDb>()
                        .WithMany()
                        .HasForeignKey("PostId"));

            modelBuilder.Entity<Category>();

            modelBuilder.Entity<CommentDb>()
                .ToTable("Comment");

            modelBuilder.Entity<User>();

            modelBuilder.Entity<Vote>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
