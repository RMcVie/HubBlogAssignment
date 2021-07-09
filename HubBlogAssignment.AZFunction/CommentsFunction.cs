using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using HubBlogAssignment.Shared;
using HubBlogAssignment.Data;
using AutoMapper;
using System;

namespace HubBlogAssignment.AZFunction
{
    public class CommentsFunction
    {
        private readonly IDataAccess dataAccess;
        private readonly IMapper mapper;

        public CommentsFunction(IDataAccess dataAccess, IMapper mapper)
        {
            this.dataAccess = dataAccess;
            this.mapper = mapper;
        }

        [Function("GetComments")]
        public async Task<HttpResponseData> Get([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Posts/{id}/Comments")] HttpRequestData req, int id, string orderBy = default)
        {
            if (await dataAccess.GetPost(id) == null)
                return req.CreateResponse(HttpStatusCode.NotFound);

            var comments = await dataAccess.GetComments(id, orderBy == default ? default : Enum.Parse<OrderBy>(orderBy)).ConfigureAwait(false);
            var response = req.CreateResponse();

            await response.WriteAsJsonAsync(mapper.Map<IEnumerable<CommentReadDto>>(comments)).ConfigureAwait(false);

            return response;
        }

        [Function("CreateComment")]
        public async Task<HttpResponseData> Post([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Posts/{id}/Comments")] HttpRequestData req, int id)
        {
            var comment = await req.ReadFromJsonAsync<CommentDb>().ConfigureAwait(false);

            if (comment == null)
                return req.CreateResponse(HttpStatusCode.BadRequest);

            await dataAccess.CreateComment(id, mapper.Map<CommentDb>(comment)).ConfigureAwait(false);
            return req.CreateResponse(HttpStatusCode.OK);
        }
    }
}
