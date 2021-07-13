using FluentAssertions;
using HubBlogAssignment.Data;
using HubBlogAssignment.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HubBlogAssignment.Tests.DataAccessTests
{
    public class PostDataAccessTests : BaseDataAccessTest
    {
        private readonly IPostAccess dataAccess;
        public PostDataAccessTests()
        {
            dataAccess = new PostAccess(new HubDbContext(dbContextOptions));
        }

        [Fact]
        public async Task AllPostsAreRetrieved()
        {
            var posts = await dataAccess.GetPosts().ConfigureAwait(false);
            posts.Count().Should().Be(3);

            var post1 = posts.Single(p => p.Id == 1);
            post1.Title.Should().Be("Test Title 1");
            post1.Summary.Should().Be("Test Summary 1");
            post1.Content.Should().Be("Test Content 1");
            post1.CreatedDateTimeUtc.Should().Be(new DateTime(2020, 1, 1));
            post1.User.Id.Should().Be(1);

            var post2 = posts.Single(p => p.Id == 2);
            post2.Title.Should().Be("Test Title 2");
            post2.Summary.Should().Be("Test Summary 2");
            post2.Content.Should().Be("Test Content 2");
            post2.CreatedDateTimeUtc.Should().Be(new DateTime(2020, 1, 2));
            post2.User.Id.Should().Be(2);
        }

        [Fact]
        public async Task PostIsRetrievedUsingId()
        {
            var post = await dataAccess.GetPost(1).ConfigureAwait(false);

            post.Title.Should().Be("Test Title 1");
            post.Summary.Should().Be("Test Summary 1");
            post.Content.Should().Be("Test Content 1");
            post.CreatedDateTimeUtc.Should().Be(new DateTime(2020, 1, 1));
            post.User.Id.Should().Be(1);
        }

        [Fact]
        public async Task NewPostIsCreated()
        {
            var post = FakeDataGenerator.FakePosts().Generate(1).Single();
            await dataAccess.CreatePost(post, new Guid("11111111-1111-1111-1111-111111111111")).ConfigureAwait(false);

            using var context = new HubDbContext(dbContextOptions);
            var insertedPost = await context.Set<PostDb>().Include(p => p.User).SingleAsync(p => p.Id == 4).ConfigureAwait(false);
            insertedPost.Summary.Should().Be(post.Summary);
            insertedPost.Content.Should().Be(post.Content);
            insertedPost.Title.Should().Be(post.Title);
            insertedPost.CreatedDateTimeUtc.Should().BeCloseTo(DateTime.UtcNow, 1000);
            insertedPost.User.AADObjectId.Should().Be("11111111-1111-1111-1111-111111111111");
        }
    }
}