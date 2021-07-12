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
    public class PostAccess : IPostAccess
    {
        private readonly HubDbContext context;
        public PostAccess(HubDbContext context)
        {
            this.context = context;
        }
        public async Task CreatePost(Post post, Guid userObjectId)
        {
            var postDb = new PostDb
            {
                Categories = post.Categories,
                Content=post.Content,
                Summary=post.Summary,
                Title=post.Title,
                User= context.Set<User>().Single(u => u.AADObjectId == userObjectId)
            };
            context.Set<PostDb>().Add(postDb);
            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<Post> GetPost(int id)
        {
            return await context.Set<PostDb>().Select(p => new Post
            {
                User = p.User,
                Categories = p.Categories,
                Content = p.Content,
                Summary = p.Summary,
                Title = p.Title,
                VotesCount = p.Votes.Count,
                Id = p.Id,
                CreatedDateTimeUtc = p.CreatedDateTimeUtc
            }).SingleAsync(p => p.Id == id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await context.Set<PostDb>().Select(p => new Post
            {
                User = p.User,
                Categories = p.Categories,
                Content = p.Content,
                Summary = p.Summary,
                Title = p.Title,
                VotesCount = p.Votes.Count,
                Id = p.Id,
                CreatedDateTimeUtc = p.CreatedDateTimeUtc
            }).ToListAsync().ConfigureAwait(false);
        }
    }
}
