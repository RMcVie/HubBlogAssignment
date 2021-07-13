using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HubBlogAssignment.Data.Entities.Database;
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
            context.Set<Vote>().Add(new Vote { Comment = comment, User = user });
            await context.SaveChangesAsync();
        }

        public async Task CreateVoteForPost(int postId, Guid userObjectId)
        {
            var post = await context.Set<PostDb>().FindAsync(postId).ConfigureAwait(false);
            var user = await context.Set<User>().SingleAsync(u => u.AadObjectId == userObjectId).ConfigureAwait(false);
            context.Set<Vote>().Add(new Vote { Post = post, User = user });
            await context.SaveChangesAsync();
        }

        public async Task DeleteVoteForComment(int commentId, Guid userObjectId)
        {
            var vote = await context.Set<Vote>().SingleAsync(v => v.Comment.Id == commentId && v.User.AadObjectId == userObjectId).ConfigureAwait(false);
            context.Set<Vote>().Remove(vote);
            await context.SaveChangesAsync();
        }

        public async Task DeleteVoteForPost(int postId, Guid userObjectId)
        {
            var vote = await context.Set<Vote>().SingleAsync(v => v.Post.Id == postId && v.User.AadObjectId == userObjectId).ConfigureAwait(false);
            context.Set<Vote>().Remove(vote);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Vote>> GetVotesForComment(int commentId)
        {
            return await context.Set<Vote>().Where(v => v.Comment.Id == commentId).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Vote>> GetVotesForPost(int postId)
        {
            return await context.Set<Vote>().Where(v => v.Post.Id == postId).ToListAsync().ConfigureAwait(false);
        }
    }
}
