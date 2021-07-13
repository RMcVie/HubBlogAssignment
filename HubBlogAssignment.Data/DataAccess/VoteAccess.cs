using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HubBlogAssignment.Data.Entities.Database;
using HubBlogAssignment.Data.Errors;
using HubBlogAssignment.Data.Interfaces;

namespace HubBlogAssignment.Data.DataAccess
{
    public class VoteAccess : IVoteAccess
    {
        private readonly HubDbContext context;

        public VoteAccess(HubDbContext context)
        {
            this.context = context;
        }

        public async Task CreateVoteForComment(int commentId, Guid userObjectId)
        {
            var comment = await context.Set<CommentDb>().FindAsync(commentId).ConfigureAwait(false);
            var user = await context.Set<User>().SingleAsync(u => u.AadObjectId == userObjectId).ConfigureAwait(false);
            var vote = new Vote {Comment = comment, User = user};

            await EnsureVoteDoesNotExist(vote);

            context.Set<Vote>().Add(vote);
            await context.SaveChangesAsync();
        }

        public async Task CreateVoteForPost(int postId, Guid userObjectId)
        {
            var post = await context.Set<PostDb>().FindAsync(postId).ConfigureAwait(false);
            var user = await context.Set<User>().SingleAsync(u => u.AadObjectId == userObjectId).ConfigureAwait(false);
            var vote = new Vote { Post = post, User = user };
            
            await EnsureVoteDoesNotExist(vote);

            context.Set<Vote>().Add(vote);
            await context.SaveChangesAsync();
        }

        public async Task DeleteVoteForComment(int commentId, Guid userObjectId)
        {
            var vote = await context.Set<Vote>().SingleOrDefaultAsync(v => v.Comment.Id == commentId && v.User.AadObjectId == userObjectId).ConfigureAwait(false);

            if (vote == null)
                throw new EntityDoesNotExistException(new Vote().GetType());

            context.Set<Vote>().Remove(vote);
            await context.SaveChangesAsync();
        }

        public async Task DeleteVoteForPost(int postId, Guid userObjectId)
        {
            var vote = await context.Set<Vote>().SingleOrDefaultAsync(v => v.Post.Id == postId && v.User.AadObjectId == userObjectId).ConfigureAwait(false);

            if (vote == null)
                throw new EntityDoesNotExistException(new Vote().GetType());

            context.Set<Vote>().Remove(vote);
            await context.SaveChangesAsync();
        }

        private async Task EnsureVoteDoesNotExist(Vote vote)
        {
            var existingVote = await context.Set<Vote>().SingleOrDefaultAsync(v=>v.User.Id == vote.User.Id && (vote.Post == null? v.Comment.Id == vote.Comment.Id : v.Post.Id == vote.Post.Id));
            if (existingVote != null)
                throw new EntityAlreadyExistsException(existingVote);
        }
    }
}
