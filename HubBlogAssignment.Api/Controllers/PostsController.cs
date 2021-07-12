using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HubBlogAssignment.Api.ExtensionMethods;
using HubBlogAssignment.Data;
using HubBlogAssignment.Data.Entities;
using HubBlogAssignment.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;

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

        [HttpGet("{id}")]
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
