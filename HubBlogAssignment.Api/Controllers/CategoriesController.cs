using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HubBlogAssignment.Data.Entities;
using HubBlogAssignment.Data.Entities.Database;
using HubBlogAssignment.Data.Errors;
using HubBlogAssignment.Data.Interfaces;
using HubBlogAssignment.Shared.DML;
using HubBlogAssignment.Shared.Read;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HubBlogAssignment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController: ControllerBase
    {
        private readonly ICategoryAccess dataAccess;
        private readonly IMapper mapper;

        public CategoriesController(ICategoryAccess dataAccess, IMapper mapper)
        {
            this.dataAccess = dataAccess;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryReadDto>> Get()
        {
            var categories = await dataAccess.GetCategories().ConfigureAwait(false);
            return mapper.Map<IEnumerable<CategoryReadDto>>(categories);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(CategoryDmlDto category)
        {
            if (category == null)
                return BadRequest();
            
            var createdCategory = mapper.Map<Category>(category);
            try
            {
                await dataAccess.CreateCategory(createdCategory).ConfigureAwait(false);
                return CreatedAtAction(nameof(Get), new {id = createdCategory.Id}, mapper.Map<CategoryReadDto>(createdCategory));
            }
            catch (EntityAlreadyExistsException)
            {
                return Conflict(new {message = "Already Exists"});
            }
        }
    }
}
