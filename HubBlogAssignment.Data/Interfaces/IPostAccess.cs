using HubBlogAssignment.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HubBlogAssignment.Data
{
    public interface IPostAccess
    {
        Task<Post> GetPost(int id);
        Task<IEnumerable<Post>> GetPosts();
        Task CreatePost(Post post, Guid userObjectId);
    }
}
