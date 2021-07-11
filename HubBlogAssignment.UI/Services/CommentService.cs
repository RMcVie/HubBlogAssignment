using HubBlogAssignment.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HubBlogAssignment.UI.Services
{
    public class CommentService : ICommentService
    {
        private readonly IHttpClientFactory httpClientFactory;
        public CommentService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task CreateComment(int postId, CommentDmlDto comment)
        {
            var client = httpClientFactory.CreateClient("HubBlog.Api.Auth");
            var resp = await client.PostAsJsonAsync($"Posts/{postId}/Comments", comment);
            resp.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<CommentReadDto>> GetComments(int postId, OrderBy OrderBy)
        {
            var client = httpClientFactory.CreateClient("HubBlog.Api.NoAuth");
            return await client.GetFromJsonAsync<IEnumerable<CommentReadDto>>($"Posts/{postId}/Comments?orderBy={OrderBy}");
        }
    }
}
