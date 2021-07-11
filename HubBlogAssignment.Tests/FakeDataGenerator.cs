using Bogus;
using HubBlogAssignment.Data.Entities;
using HubBlogAssignment.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubBlogAssignment.Data
{
    public static class FakeDataGenerator
    {
        public static readonly string[] Categories = { "Tech", "Finance", "Sports" };
        public static Faker<Post> FakePosts()
        {
            return new Faker<Post>()
                .StrictMode(true)
                .RuleFor(p => p.Title, f => f.Lorem.Sentence())
                .RuleFor(p => p.Summary, f => f.Lorem.Sentence())
                .RuleFor(p => p.Content, f => f.Lorem.Paragraphs())
                .RuleFor(p => p.CreatedDateTimeUtc, f => f.Date.Recent())
                .RuleFor(p => p.Id, f => f.IndexFaker)
                .Ignore(p=>p.User)
                .Ignore(p=>p.Categories)
                .Ignore(p => p.Comments);
        }

        public static Faker<Comment> FakeComments(DateTime postCreateTimeUtc)
        {
            return new Faker<Comment>()
                .StrictMode(true)
                .RuleFor(c => c.Content, f => f.Lorem.Paragraphs())
                .RuleFor(c => c.CreatedDateTimeUtc, f => f.Date.Between(postCreateTimeUtc, DateTime.UtcNow))
                .RuleFor(c => c.Id, f => f.IndexFaker)
                .RuleFor(c => c.Score, f => f.Random.Int(0, 100))
                .Ignore(c => c.Post)
                .Ignore(c => c.User);
        }

        public static Faker<User> FakeUser()
        {
            return new Faker<User>()
                .StrictMode(true)
                .Ignore(u => u.Id)
                .RuleFor(u => u.DisplayName, f => f.Name.FullName())
                .RuleFor(u => u.AADObjectId, f => Guid.NewGuid())
                .Ignore(u => u.Comments)
                .Ignore(u => u.Posts);
        }
    }
}
