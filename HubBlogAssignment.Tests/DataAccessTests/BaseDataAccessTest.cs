using System;
using HubBlogAssignment.Data;
using HubBlogAssignment.Data.Entities.Database;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HubBlogAssignment.Tests.DataAccessTests
{
    [Collection("DatabaseTests")]
    public abstract class BaseDataAccessTest
    {
        protected const string ConnectionString = "Server=localhost;Database=HubBlogAssignmentTest;Integrated Security=true";
        protected readonly DbContextOptions<HubDbContext> DbContextOptions;

        protected BaseDataAccessTest()
        {
            DbContextOptions = new DbContextOptionsBuilder<HubDbContext>().UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EFSample.Testing;Trusted_Connection=True;").Options;
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using var context = new HubDbContext(DbContextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var user1 = new User { AadObjectId = new Guid("11111111-1111-1111-1111-111111111111"), DisplayName = "Test Display Name 1" };
            var user2 = new User { AadObjectId = new Guid("22222222-2222-2222-2222-222222222222"), DisplayName = "Test Display Name 2" };
            var user3 = new User { AadObjectId = new Guid("33333333-3333-3333-3333-333333333333"), DisplayName = "Test Display Name 3" };

            var category1 = new Category { Name = "Finance" };
            var category2 = new Category { Name = "Tech" };

            var vote1 = new Vote { CreatedDateTimeUtc = new DateTime(20, 1, 7), User = user1 };
            var vote2 = new Vote { CreatedDateTimeUtc = new DateTime(20, 1, 8), User = user3 };
            var vote3 = new Vote { CreatedDateTimeUtc = new DateTime(20, 1, 9), User = user1 };
            var vote4 = new Vote { CreatedDateTimeUtc = new DateTime(20, 1, 10), User = user2 };
            var vote5 = new Vote { CreatedDateTimeUtc = new DateTime(20, 1, 11), User = user2 };

            var comment1 = new CommentDb { Content = "Test Content 1", CreatedDateTimeUtc = new DateTime(20, 1, 4), User = user1, Votes = new[] { vote5 } };
            var comment2 = new CommentDb { Content = "Test Content 2", CreatedDateTimeUtc = new DateTime(20, 1, 5), User = user1 };
            var comment3 = new CommentDb { Content = "Test Content 3", CreatedDateTimeUtc = new DateTime(20, 1, 6), User = user2, Votes = new[] { vote1, vote2} };

            var post1 = new PostDb
            {
                Title = "Test Title 1",
                Summary = "Test Summary 1",
                Content = "Test Content 1",
                CreatedDateTimeUtc = new DateTime(2020, 1, 1),
                User = user1,
                Categories = new[] { category1 },
                Comments = new [] { comment1, comment2 },
                Votes = new[] {vote3, vote4}
            };
            var post2 = new PostDb
            {
                Title = "Test Title 2",
                Summary = "Test Summary 2",
                Content = "Test Content 2",
                CreatedDateTimeUtc = new DateTime(2020, 1, 2),
                User = user2,
                Categories = new[] { category1, category2 },
                Comments = new[] { comment3 }
            };
            var post3 = new PostDb
            {
                Title = "Test Title 3",
                Summary = "Test Summary 3",
                Content = "Test Content 3",
                CreatedDateTimeUtc = new DateTime(2020, 1, 3),
                User = user3,
            };

            context.Set<PostDb>().AddRange(post1, post2, post3);
            context.SaveChanges();
        }
    }
}