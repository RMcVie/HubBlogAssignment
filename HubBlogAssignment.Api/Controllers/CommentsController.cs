using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HubBlogAssignment.Api.ExtensionMethods;
using HubBlogAssignment.Data.Entities;
using HubBlogAssignment.Data.Interfaces;
using HubBlogAssignment.Shared;
using HubBlogAssignment.Shared.DML;
using HubBlogAssignment.Shared.Read;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HubBlogAssignment.Api.Controllers
{
    [ApiController]
    [Route("/Posts/{postId:int}/Comments")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentAccess dataAccess;
        private readonly IMapper mapper;

        public CommentsController(ICommentAccess dataAccess, IMapper mapper)
        {
            this.dataAccess = dataAccess;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CommentReadDto>> Get(int postId, OrderBy orderBy)
        {
            var comments = await dataAccess.GetComments(postId, orderBy).ConfigureAwait(false);
            return mapper.Map<IEnumerable<CommentReadDto>>(comments);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateComment(int postId, CommentDmlDto comment)
        {
            if (comment == null)
                return BadRequest();

            var createdComment = mapper.Map<Comment>(comment);
            var id = await dataAccess.CreateComment(postId, createdComment, HttpContext.User.GetAadObjectId()).ConfigureAwait(false);

            return CreatedAtAction(nameof(Get), new { id }, mapper.Map<CommentReadDto>(createdComment));
        }
    }
}
