//using HubBlogAssignment.Data;
//using HubBlogAssignment.Data.Entities;
//using HubBlogAssignment.Shared;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace HubBlogAssignment.Tests.DataAccessTests
//{
//    public class InitLiveData : BaseDataAccessTest
//    {
//        [Fact]
//        public void temp()
//        {
//            using var context = new HubDbContext(dbContextOptions);
//            context.Database.ExecuteSqlRaw("TRUNCATE TABLE [Vote]; TRUNCATE TABLE [PostCategory]; DELETE FROM [Comment]; DELETE FROM [Post]; DELETE FROM [Category]; DELETE FROM [User];");
//            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Post', RESEED, 0);");
//            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('User', RESEED, 0);");
//            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Category', RESEED, 0);");
//            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Comment', RESEED, 0);");

//            var users = FakeDataGenerator.FakeUser().Generate(1000);
//            var posts = new List<Post>();
//            foreach (var post in FakeDataGenerator.FakePosts().Generate(100))
//            {
//                post.Categories = FakeDataGenerator.FakeCategories().Generate(new Random().Next(1, 3));
//                post.User = users.OrderBy(x => Guid.NewGuid()).First();
//                post.Comments = FakeDataGenerator.FakeComments(post.CreatedDateTimeUtc).Generate(new Random().Next(10, 50));
//                foreach (var comment in post.Comments)
//                {
//                    comment.User = users.OrderBy(x => Guid.NewGuid()).First();
//                    comment.Votes = FakeDataGenerator.FakeVotes().Generate(new Random().Next(0, 100)).ToList();

//                    foreach (var (vote, user) in comment.Votes.Zip(users.OrderBy(x => Guid.NewGuid()).Take(comment.Votes.Count).ToList()))
//                    {
//                        vote.User = user;
//                    }
//                }
//                posts.Add(post);
//                post.Votes = FakeDataGenerator.FakeVotes().Generate(new Random().Next(0, 100));

//                foreach (var (vote, user) in post.Votes.Zip(users.OrderBy(x => Guid.NewGuid()).Take(post.Votes.Count).ToList()))
//                {
//                    vote.User = user;
//                }
//            }
//            context.Set<Post>().AddRange(posts);
//            context.SaveChanges();
//        }
//    }
//}
