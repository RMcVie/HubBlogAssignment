using HubBlogAssignment.Shared;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HubBlogAssignment.UI.Services
{
    public class PostService : IPostService
    {
        private readonly IHttpClientFactory httpClientFactory;
        public PostService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<PostReadDto> GetPost(int id)
        {
            var client = httpClientFactory.CreateClient("HubBlog.Api.NoAuth");
            return await client.GetFromJsonAsync<PostReadDto>($"Posts/{id}");
        }

        public async Task<IEnumerable<PostReadDto>> GetPosts()
        {
            var client = httpClientFactory.CreateClient("HubBlog.Api.NoAuth");
            return await client.GetFromJsonAsync<PostReadDto[]>("Posts");
        }
    }
}
