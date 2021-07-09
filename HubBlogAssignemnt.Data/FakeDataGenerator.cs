using Bogus;
using HubBlogAssignment.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubBlogAssignemnt.Data
{
    public static class FakeDataGenerator
    {
        public static readonly string[] Categories = { "Tech", "Finance", "Sports" };
        public static Faker<PostDb> FakePosts()
        {
            return new Faker<PostDb>()
                .StrictMode(true)
                .RuleFor(p => p.CreatedUser, f => f.Name.FullName())
                .RuleFor(p => p.Title, f => f.Lorem.Sentence())
                .RuleFor(p => p.Summary, f => f.Lorem.Sentence())
                .RuleFor(p => p.Content, f => f.Lorem.Paragraphs())
                .RuleFor(p => p.Category, f => f.PickRandom(Categories))
                .RuleFor(p => p.CreatedDateTimeUtc, f => f.Date.Recent())
                .RuleFor(p => p.PostId, f => f.IndexFaker)
                .Ignore(p => p.Comments);
        }

        public static Faker<CommentDb> FakeComments(int postId, DateTime postCreateTimeUtc)
        {
            return new Faker<CommentDb>()
                .StrictMode(true)
                .RuleFor(p => p.CreatedUser, f => f.Name.FullName())
                .RuleFor(p => p.Content, f => f.Lorem.Paragraphs())
                .RuleFor(p => p.CreatedDateTimeUtc, f => f.Date.Between(postCreateTimeUtc, DateTime.UtcNow))
                .RuleFor(p => p.PostId, f => postId)
                .RuleFor(p => p.CommentId, f => f.IndexFaker)
                .RuleFor(p=>p.Score, f=>f.Random.Int(0, 100));
        }
    }
}
