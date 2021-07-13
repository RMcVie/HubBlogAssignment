using System;
using Bogus;
using HubBlogAssignment.Data.Entities;
using HubBlogAssignment.Data.Entities.Database;

namespace HubBlogAssignment.Tests
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
                .Ignore(p => p.Id)
                .Ignore(p => p.User)
                .Ignore(p => p.Categories)
                .Ignore(p => p.VotesCount);
        }

        public static Faker<Comment> FakeComments(DateTime postCreateTimeUtc)
        {
            return new Faker<Comment>()
                .StrictMode(true)
                .RuleFor(c => c.Content, f => f.Lorem.Sentence())
                .RuleFor(c => c.CreatedDateTimeUtc, f => f.Date.Between(postCreateTimeUtc, DateTime.UtcNow))
                .Ignore(p => p.Id)
                .Ignore(c => c.VotesCount)
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
                .Ignore(u => u.Posts)
                .RuleFor(x => x.CreatedDateTimeUtc, f => f.Date.Past());
        }

        public static Faker<Category> FakeCategories()
        {
            return new Faker<Category>()
                .StrictMode(true)
                .RuleFor(x => x.Name, f => f.Random.Word())
                .Ignore(u => u.Id)
                .RuleFor(x => x.CreatedDateTimeUtc, f => f.Date.Past())
                .Ignore(x => x.Posts);
        }

        public static Faker<Vote> FakeVotes()
        {
            return new Faker<Vote>()
                .StrictMode(true)
                .RuleFor(v => v.CreatedDateTimeUtc, f => f.Date.Past())
                .Ignore(v => v.Comment)
                .Ignore(v => v.Id)
                .Ignore(v => v.Post)
                .Ignore(v => v.User);
        }

        //public static IEnumerable<Post> FakePostsIncludingSubEntities()
        //{
        //    var users = FakeUser().Generate(100);
        //    var categories = new Faker<Category>()
        //        .StrictMode(true)
        //        .RuleFor(x => x.Name, f => f.Random.Word())
        //        .Ignore(u => u.Id)
        //        .RuleFor(x => x.CreatedDateTimeUtc, f => f.Date.Past())
        //        .Ignore(x => x.Posts).Generate(100);

        //    var comments = new Faker<Comment>()
        //        .StrictMode(true)
        //        .RuleFor(c => c.Content, f => f.Lorem.Paragraphs())
        //        .RuleFor(c => c.CreatedDateTimeUtc, f => f.Date.Past())
        //        .Ignore(c => c.Id)
        //        .RuleFor(c => c.Score, f => f.Random.Int(0, 100))
        //        .Ignore(c => c.Post)
        //        .RuleFor(c => c.User, f=>f.PickRandom(users))
        //        .Generate(1000);

        //    return new Faker<Post>()
        //        .StrictMode(true)
        //        .RuleFor(p => p.Title, f => f.Lorem.Sentence(4))
        //        .RuleFor(p => p.Summary, f => f.Lorem.Sentence())
        //        .RuleFor(p => p.Content, f => f.Lorem.Paragraphs())
        //        .RuleFor(p => p.CreatedDateTimeUtc, f => f.Date.Recent())
        //        .RuleFor(p => p.Id, f => f.IndexFaker)
        //        .RuleFor(p => p.User, f => f.PickRandom(users))
        //        .RuleFor(p => p.Categories, f => categories.OrderBy(a => Guid.NewGuid()).ToList().Take(f.Random.Int(1, 3)).ToList())
        //        .RuleFor(p => p.Comments, f => comments.OrderBy(a => Guid.NewGuid()).ToList().Take(f.Random.Int(1, 10))).Generate(100);
        //}
    }
}
