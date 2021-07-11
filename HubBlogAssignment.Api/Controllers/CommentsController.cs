using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HubBlogAssignment.Data;
using HubBlogAssignment.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;

namespace HubBlogAssignment.Api.Controllers
{
    [ApiController]
    [Route("/Posts/{id}/Comments")]
    public class CommentsController : ControllerBase
    {
        private readonly IDataAccess dataAccess;
        private readonly IMapper mapper;

        public CommentsController(IDataAccess dataAccess, IMapper mapper)
        {
            this.dataAccess = dataAccess;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CommentReadDto>> Get(int id, OrderBy orderBy)
        {
            var comments = await dataAccess.GetComments(id, orderBy).ConfigureAwait(false);
            return mapper.Map<IEnumerable<CommentReadDto>>(comments);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(int id, CommentDmlDto comment)
        {
            if (comment == null)
                return BadRequest();

            var commentDb = mapper.Map<Comment>(comment);
            var createdComment = await dataAccess.CreateComment(id, commentDb, Guid.NewGuid()).ConfigureAwait(false);

            return CreatedAtAction(nameof(Get), new { id = createdComment.Id }, mapper.Map<CommentReadDto>(createdComment));
        }
    }
}
