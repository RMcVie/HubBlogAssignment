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
        private readonly HttpClient httpClient;
        public CommentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task CreateComment(int postId, CommentDmlDto comment)
        {
            var resp = await httpClient.PostAsJsonAsync($"Posts/{postId}/Comments", comment);
            resp.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<CommentReadDto>> GetComments(int postId, OrderBy OrderBy)
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<CommentReadDto>>($"Posts/{postId}/Comments?orderBy={OrderBy}");
        }
    }
}
