using System.Collections.Generic;
using System.Threading.Tasks;
using HubBlogAssignment.Shared.Read;

namespace HubBlogAssignment.UI.Services
{
    public interface IPostService
    {
        Task<IEnumerable<PostReadDto>> GetPosts();
        Task<PostReadDto> GetPost(int id);
    }
}
