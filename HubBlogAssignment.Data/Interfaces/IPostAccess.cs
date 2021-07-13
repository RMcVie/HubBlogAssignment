using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HubBlogAssignment.Data.Entities;

namespace HubBlogAssignment.Data.Interfaces
{
    public interface IPostAccess
    {
        Task<Post> GetPost(int id);
        Task<IEnumerable<Post>> GetPosts();
        Task<int> CreatePost(Post post, Guid userObjectId);
    }
}
