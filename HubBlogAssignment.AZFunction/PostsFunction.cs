using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using HubBlogAssignment.Shared;
using HubBlogAssignment.Data;
using AutoMapper;

namespace HubBlogAssignment.AZFunction
{
    public class PostsFunction
    {
        private readonly IDataAccess dataAccess;
        private readonly IMapper mapper;

        public PostsFunction(IDataAccess dataAccess, IMapper mapper)
        {
            this.dataAccess = dataAccess;
            this.mapper = mapper;
        }

        [Function("GetPosts")]
        public async Task<HttpResponseData> GetPosts([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Posts")] HttpRequestData req)
        {
            var posts = await dataAccess.GetPosts().ConfigureAwait(false);
            var response = req.CreateResponse();
            await response.WriteAsJsonAsync(mapper.Map<IEnumerable<PostReadDto>>(posts)).ConfigureAwait(false);

            return response;
        }

        [Function("GetPost")]
        public async Task<HttpResponseData> GetPost([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Posts/{id}")] HttpRequestData req, int id)
        {
            var post = await dataAccess.GetPost(id).ConfigureAwait(false);
            var response = req.CreateResponse();
            await response.WriteAsJsonAsync(mapper.Map<PostReadDto>(post)).ConfigureAwait(false);

            return response;
        }

        [Function("CreatePost")]
        public async Task<HttpResponseData> Post([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Posts")] HttpRequestData req)
        {
            var post = await req.ReadFromJsonAsync<PostDmlDto>().ConfigureAwait(false);

            if (post == null)
                return req.CreateResponse(HttpStatusCode.BadRequest);

            await dataAccess.CreatePost(mapper.Map<PostDb>(post)).ConfigureAwait(false);
            return req.CreateResponse(HttpStatusCode.OK);
        }
    }
}
