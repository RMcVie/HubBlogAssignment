using HubBlogAssignment.Data.Entities;
using HubBlogAssignment.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
