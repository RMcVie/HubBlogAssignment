using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HubBlogAssignment.Api.ExtensionMethods;
using HubBlogAssignment.Data;
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
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostAccess dataAccess;
        private readonly IMapper mapper;

        public PostsController(IPostAccess dataAccess, IMapper mapper)
        {
            this.dataAccess = dataAccess;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<PostReadDto>> Get()
        {
            var posts = await dataAccess.GetPosts().ConfigureAwait(false);
            return mapper.Map<IEnumerable<PostReadDto>>(posts);
        }

        [HttpGet("{id:int}")]
        public async Task<PostReadDto> Get(int id)
        {
            var posts = await dataAccess.GetPost(id).ConfigureAwait(false);
            return mapper.Map<PostReadDto>(posts);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost(PostDmlDto post)
        {
            if (post == null)
                return BadRequest();

            var createdPost = mapper.Map<Post>(post);
            await dataAccess.CreatePost(createdPost, HttpContext.User.GetAadObjectId()).ConfigureAwait(false);
            return CreatedAtAction(nameof(Get), new { id = createdPost.Id }, mapper.Map<PostReadDto>(createdPost));
        }
    }
}
