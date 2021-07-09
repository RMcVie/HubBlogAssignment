using HubBlogAssignment.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HubBlogAssignemnt.UI.Services
{
    public interface IPostService
    {
        Task<IEnumerable<PostReadDto>> GetPosts();
        Task<PostReadDto> GetPost(int id);
    }
}
