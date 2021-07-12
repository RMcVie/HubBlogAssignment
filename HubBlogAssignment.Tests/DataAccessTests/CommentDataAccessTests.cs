using FluentAssertions;
using HubBlogAssignment.Data;
using HubBlogAssignment.Data.Entities;
using HubBlogAssignment.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HubBlogAssignment.Tests.DataAccessTests
{
    public class CommentDataAccessTests : BaseDataAccessTest
    {
        private readonly ICommentAccess dataAccess;
        public CommentDataAccessTests()
        {
            dataAccess = new CommentAccess(new HubDbContext(dbContextOptions));
        }

        [Fact]
        public async Task CommentsAreRetrievedForPost()
        {
            var comments = await dataAccess.GetComments(2, default).ConfigureAwait(false);
            comments.Count().Should().Be(1);
            comments.Single().Content.Should().Be("Test Content 3");
            comments.Single().VotesCount.Should().Be(2);
            comments.Single().CreatedDateTimeUtc.Should().Be(new DateTime(20, 1, 6));
            comments.Single().User.DisplayName.Should().Be("Test Display Name 2");
        }

        [Fact]
        public async Task CommentsAreOrderedByNewCorrectly()
        {
            var comments = await dataAccess.GetComments(1, OrderBy.New).ConfigureAwait(false);
            comments.Count().Should().Be(2);
            comments.First().CreatedDateTimeUtc.Should().BeAfter(comments.Last().CreatedDateTimeUtc);
        }

        [Fact]
        public async Task CommentsAreOrderedByTopCorrectly()
        {
            var comments = await dataAccess.GetComments(1, OrderBy.Top).ConfigureAwait(false);
            comments.Count().Should().Be(2);
            comments.First().VotesCount.Should().BeGreaterThan(comments.Last().VotesCount);
        }

        [Fact]
        public async Task NewCommentIsCreated()
        {
            var comment = FakeDataGenerator.FakeComments(DateTime.UtcNow).Generate(1).Single();
            await dataAccess.CreateComment(1, comment, new Guid("11111111-1111-1111-1111-111111111111")).ConfigureAwait(false);

            using var context = new HubDbContext(dbContextOptions);
            var insertedComment = await context.Set<CommentDb>().Include(c => c.Post).Include(c => c.User).SingleAsync(c => c.Id == 4).ConfigureAwait(false);
            insertedComment.Content.Should().Be(comment.Content);
            insertedComment.Post.Id.Should().Be(1);
            insertedComment.User.AADObjectId.Should().Be("11111111-1111-1111-1111-111111111111");
        }
    }
}