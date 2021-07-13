using HubBlogAssignment.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using HubBlogAssignment.Data.Entities.Database;
using Xunit;

namespace HubBlogAssignment.Tests.DataAccessTests
{
    public class InitLiveData : BaseDataAccessTest
    {
        [Fact]
        public void Temp()
        {
            using var context = new HubDbContext(DbContextOptions);
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE [Vote]; TRUNCATE TABLE [PostCategory]; DELETE FROM [Comment]; DELETE FROM [Post]; DELETE FROM [Category]; DELETE FROM [User];");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Post', RESEED, 0);");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('User', RESEED, 0);");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Category', RESEED, 0);");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Comment', RESEED, 0);");
            var rand = new Random();
            var users = FakeDataGenerator.FakeUser().Generate(20).ToList();
            var posts = new List<PostDb>();
            foreach (var post in FakeDataGenerator.FakePosts().Generate(15))
            {
                var postDb = new PostDb {
                    Title = post.Title,
                    Summary = post.Summary,
                    Content = post.Content,
                    Categories = FakeDataGenerator.FakeCategories().Generate(new Random().Next(1, 3)),
                    User = users[rand.Next(users.Count)],
                    CreatedDateTimeUtc = post.CreatedDateTimeUtc
                };
                var comments = FakeDataGenerator.FakeComments(post.CreatedDateTimeUtc).Generate(new Random().Next(3, 10));
                foreach (var comment in comments)
                {
                    postDb.Comments.Add(new CommentDb { 
                        CreatedDateTimeUtc = comment.CreatedDateTimeUtc,
                        Content = comment.Content,
                        User = users[rand.Next(users.Count)],
                        Post = postDb
                    });

                    var votes = FakeDataGenerator.FakeVotes().Generate(new Random().Next(0, 10)).ToList();
                    var votingUsers = users.OrderBy(arg => Guid.NewGuid()).Take(votes.Count).ToList();

                    foreach (var (vote, user) in votes.Zip(votingUsers))
                    {
                        vote.User = user;
                        postDb.Comments.Last().Votes.Add(vote);    
                    }
                }
                var postVotes = FakeDataGenerator.FakeVotes().Generate(new Random().Next(0, 10)).ToList();
                var postVotingUsers = users.OrderBy(arg => Guid.NewGuid()).Take(postVotes.Count).ToList();

                foreach (var (vote, user) in postVotes.Zip(postVotingUsers))
                {
                    vote.User = user;
                    postDb.Votes.Add(vote);
                }
                posts.Add(postDb);
            }
            context.Set<PostDb>().AddRange(posts);
            context.SaveChanges();
        }
    }
}
