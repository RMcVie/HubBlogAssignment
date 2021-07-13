using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using HubBlogAssignment.Data;
using HubBlogAssignment.Data.DataAccess;
using HubBlogAssignment.Data.Entities.Database;
using HubBlogAssignment.Data.Errors;
using HubBlogAssignment.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HubBlogAssignment.Tests.DataAccessTests
{
    public class VoteDataAccessTests : BaseDataAccessTest
    {
        private readonly IVoteAccess voteAccess;
        private readonly Dictionary<string, int> votesSource;
        private readonly Guid userGuid = new("11111111-1111-1111-1111-111111111111");

        public VoteDataAccessTests()
        {
            voteAccess = new VoteAccess(new HubDbContext(DbContextOptions));
            votesSource = new Dictionary<string, int>
            {
                {"NewPostVote", 2},
                {"NewCommentVote", 1},
                {"ExistingPostVote", 1}, 
                {"ExistingCommentVote", 3}
            };
        }

        [Fact]
        public async Task VoteIsCreatedForPost()
        {
            var postId = votesSource["NewPostVote"];
            await voteAccess.CreateVoteForPost(postId, userGuid).ConfigureAwait(false);

            await using var context = new HubDbContext(DbContextOptions);
            var insertedVote = await context.Set<Vote>().SingleOrDefaultAsync(v => v.User.AadObjectId == userGuid && v.Post.Id == postId).ConfigureAwait(false);
            insertedVote.Should().NotBeNull();
        }

        [Fact]
        public async Task VoteIsCreatedForComment()
        {
            var commentId = votesSource["NewCommentVote"];
            await voteAccess.CreateVoteForComment(commentId, userGuid).ConfigureAwait(false);

            await using var context = new HubDbContext(DbContextOptions);
            var insertedVote = await context.Set<Vote>().SingleOrDefaultAsync(v => v.User.AadObjectId == userGuid && v.Comment.Id == commentId).ConfigureAwait(false);
            insertedVote.Should().NotBeNull();
        }

        [Fact]
        public async Task VoteIsRemovedForPost()
        {
            var postId = votesSource["ExistingPostVote"];
            await voteAccess.DeleteVoteForPost(postId, userGuid).ConfigureAwait(false);

            await using var context = new HubDbContext(DbContextOptions);
            var insertedVote = await context.Set<Vote>().SingleOrDefaultAsync(v => v.User.AadObjectId == userGuid && v.Post.Id == postId).ConfigureAwait(false);
            insertedVote.Should().BeNull();
        }

        [Fact]
        public async Task VoteIsRemovedForComment()
        {
            var commentId = votesSource["ExistingCommentVote"];
            await voteAccess.DeleteVoteForComment(commentId, userGuid).ConfigureAwait(false);

            await using var context = new HubDbContext(DbContextOptions);
            var insertedVote = await context.Set<Vote>().SingleOrDefaultAsync(v => v.User.AadObjectId == userGuid && v.Comment.Id == commentId).ConfigureAwait(false);
            insertedVote.Should().BeNull();
        }

        [Fact]
        public void VotingTwiceOnSamePostThrowsError()
        {
            var postId = votesSource["ExistingPostVote"];
            Func<Task> action = async () => await voteAccess.CreateVoteForPost(postId, userGuid).ConfigureAwait(false);
            action.Should().Throw<EntityAlreadyExistsException>().WithMessage("Entity of Type HubBlogAssignment.Data.Entities.Database.Vote Already Exists!");
        }

        [Fact]
        public void VotingTwiceOnSameCommentThrowsError()
        {
            var commentId = votesSource["ExistingCommentVote"];
            Func<Task> action = async () => await voteAccess.CreateVoteForComment(commentId, userGuid).ConfigureAwait(false);
            action.Should().Throw<EntityAlreadyExistsException>().WithMessage("Entity of Type HubBlogAssignment.Data.Entities.Database.Vote Already Exists!");
        }

        [Fact]
        public void RemovingNonExistentPostVoteThrowsError()
        {
            var postId = votesSource["NewPostVote"];
            Func<Task> action = async () => await voteAccess.DeleteVoteForPost(postId, userGuid).ConfigureAwait(false);
            action.Should().Throw<EntityDoesNotExistException>().WithMessage("Entity of Type HubBlogAssignment.Data.Entities.Database.Vote Does Not Exist!");
        }

        [Fact]
        public void RemovingNonExistentCommentVoteThrowsError()
        {
            var commentId = votesSource["NewCommentVote"];
            Func<Task> action = async () => await voteAccess.DeleteVoteForComment(commentId, userGuid).ConfigureAwait(false);
            action.Should().Throw<EntityDoesNotExistException>().WithMessage("Entity of Type HubBlogAssignment.Data.Entities.Database.Vote Does Not Exist!");
        }
    }
}