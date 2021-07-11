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
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IDataAccess dataAccess;
        private readonly IMapper mapper;

        public PostsController(IDataAccess dataAccess, IMapper mapper)
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
        public async Task<IActionResult> Post(PostDmlDto post)
        {
            if (post == null)
                return BadRequest();

            var createdPost = await dataAccess.CreatePost(mapper.Map<Post>(post), Guid.NewGuid()).ConfigureAwait(false);
            return CreatedAtAction(nameof(Get), new { id = createdPost.Id }, mapper.Map<PostReadDto>(createdPost));
        }
    }
}
