using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HubBlogAssignment.Data.Entities;
using HubBlogAssignment.Data.Entities.Database;
using HubBlogAssignment.Data.Interfaces;
using HubBlogAssignment.Shared;
using Microsoft.EntityFrameworkCore;

namespace HubBlogAssignment.Data.DataAccess
{
    public class CommentAccess : ICommentAccess
    {
        private readonly HubDbContext context;
        public CommentAccess(HubDbContext context)
        {
            this.context = context;
        }

        public async Task CreateComment(int postId, Comment comment, Guid userObjectId)
        {
            var user = context.Set<User>().Single(u => u.AADObjectId == userObjectId);
            var post = await context.Set<PostDb>().FindAsync(postId).ConfigureAwait(false);

            post.Comments.Add(new CommentDb { Content = comment.Content, Post = post, User = user});

            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Comment>> GetComments(int postId, OrderBy orderBy)
        {
            var query = context.Set<CommentDb>().Where(c => c.Post.Id == postId)
                .Select(c => new Comment
                {
                    Content = c.Content,
                    VotesCount = c.Votes.Count,
                    CreatedDateTimeUtc = c.CreatedDateTimeUtc,
                    User = c.User,
                    Id = c.Id
                });

            return orderBy switch
            {
                OrderBy.New => await query.OrderByDescending(c => c.CreatedDateTimeUtc).ToListAsync().ConfigureAwait(false),
                OrderBy.Top => await query.OrderByDescending(c => c.VotesCount).ToListAsync().ConfigureAwait(false),
                _ => throw new InvalidOperationException($"Value {orderBy} is not a valid OrderBy Enum Value"),
            };
        }
    }
}
