using HubBlogAssignment.Shared;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HubBlogAssignment.UI.Services
{
    public class PostService : IPostService
    {
        private readonly HttpClient httpClient;
        public PostService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<PostReadDto> GetPost(int id)
        {
            return await httpClient.GetFromJsonAsync<PostReadDto>($"Posts/{id}");
        }

        public async Task<IEnumerable<PostReadDto>> GetPosts()
        {
            return await httpClient.GetFromJsonAsync<PostReadDto[]>("Posts");
        }
    }
}
