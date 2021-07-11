using HubBlogAssignment.Data.Entities;
using HubBlogAssignment.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubBlogAssignment.Data
{
    public class DataAccess : IDataAccess
    {
        private readonly HubDbContext context;
        public DataAccess(HubDbContext context)
        {
            this.context = context;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            context.Set<Category>().Add(category);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return category;
        }

        public async Task<Comment> CreateComment(int postId, Comment comment, Guid userObjectId)
        {
            var user = context.Set<User>().Single(u => u.AADObjectId == userObjectId);
            comment.User = user;
            var post = await context.Set<Post>().FindAsync(postId).ConfigureAwait(false);
            post.Comments.Add(comment);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return comment;
        }

        public async Task<Post> CreatePost(Post post, Guid userObjectId)
        {
            var user = context.Set<User>().Single(u => u.AADObjectId == userObjectId);
            post.User = user;
            context.Set<Post>().Add(post);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return post;
        }

        public Task DeleteComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task DeletePost(Post post)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetCategories()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Comment>> GetComments(int postId, OrderBy orderBy)
        {
            return orderBy switch
            {
                OrderBy.New => await context.Set<Comment>().Where(c => c.Post.Id == postId).Include(c => c.User).OrderByDescending(c => c.CreatedDateTimeUtc).ToListAsync().ConfigureAwait(false),
                OrderBy.Top => await context.Set<Comment>().Where(c => c.Post.Id == postId).Include(c => c.User).OrderByDescending(c => c.Score).ToListAsync().ConfigureAwait(false),
                _ => throw new InvalidOperationException($"Value {orderBy} is not a valid OrderBy Enum Value"),
            };
        }

        public async Task<Post> GetPost(int id)
        {
            return await context.Set<Post>().Include(p => p.User).SingleAsync(p=>p.Id == id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await context.Set<Post>().Include(p => p.User).ToListAsync().ConfigureAwait(false);
        }
    }
}
