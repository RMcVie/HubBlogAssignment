using FluentAssertions;
using HubBlogAssignment.Data;
using HubBlogAssignment.Data.Entities;
using HubBlogAssignment.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HubBlogAssignment.Tests.DataAccessTests
{
    [Collection("DatabaseTests")]
    public abstract class BaseDataAccessTest
    {
        protected const string ConnectionString = "Server=localhost;Database=HubBlogAssignment;Integrated Security=true";
        protected readonly DbContextOptions<HubDbContext> dbContextOptions;
        protected readonly IDataAccess dataAccess;

        public BaseDataAccessTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<HubDbContext>().UseSqlServer(ConnectionString).Options;
            dataAccess = new DataAccess(new HubDbContext(dbContextOptions));
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using var context = new HubDbContext(dbContextOptions);
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE [PostCategory]; TRUNCATE TABLE [Comment]; DELETE FROM [Post]; DELETE FROM [Category]; DELETE FROM [User];");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Post', RESEED, 0);");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('User', RESEED, 0);");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Category', RESEED, 0);");

            var user1 = new User() { AADObjectId = new Guid("11111111-1111-1111-1111-111111111111"), DisplayName = "Test Display Name 1" };
            var user2 = new User() { AADObjectId = new Guid("22222222-2222-2222-2222-222222222222"), DisplayName = "Test Display Name 2" };

            var category1 = new Category() { Name = "Finance" };
            var category2 = new Category() { Name = "Tech" };

            var comment1 = new Comment() { Content = "Test Content 1", Score = 1, CreatedDateTimeUtc = new DateTime(20, 1, 3), User = user1 };
            var comment2 = new Comment() { Content = "Test Content 2", Score = 2, CreatedDateTimeUtc = new DateTime(20, 1, 4), User = user1 };
            var comment3 = new Comment() { Content = "Test Content 3", Score = 3, CreatedDateTimeUtc = new DateTime(20, 1, 5), User = user2 };

            var post1 = new Post()
            {
                Title = "Test Title 1",
                Summary = "Test Summary 1",
                Content = "Test Content 1",
                CreatedDateTimeUtc = new DateTime(2020, 1, 1),
                User = user1,
                Categories = new List<Category>() { category1 },
                Comments = new List<Comment>() { comment1, comment2 }
            };
            var post2 = new Post()
            {
                Title = "Test Title 2",
                Summary = "Test Summary 2",
                Content = "Test Content 2",
                CreatedDateTimeUtc = new DateTime(2020, 1, 2),
                User = user2,
                Categories = new List<Category>() { category1, category2 },
                Comments = new List<Comment>() { comment3 }
            };

            context.Set<Post>().AddRange(new Post[] { post1, post2 });
            context.SaveChanges();
        }
    }
}