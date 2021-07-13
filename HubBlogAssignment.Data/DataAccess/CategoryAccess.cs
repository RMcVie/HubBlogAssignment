using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using HubBlogAssignment.Data.Entities;
using HubBlogAssignment.Data.Entities.Database;
using HubBlogAssignment.Data.Errors;
using HubBlogAssignment.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HubBlogAssignment.Data.DataAccess
{
    public class CategoryAccess : ICategoryAccess
    {
        private readonly HubDbContext context;

        public CategoryAccess(HubDbContext context)
        {
            this.context = context;
        }

        public async Task CreateCategory(Category category)
        {
            await AssertCategoryDoesNotExist(category);
            context.Set<Category>().Add(category);
            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await context.Set<Category>().ToListAsync().ConfigureAwait(false);
        }

        private async Task AssertCategoryDoesNotExist(Category category)
        {
            var existingCategory = await context.Set<Category>().SingleOrDefaultAsync(c=>c.Name == category.Name).ConfigureAwait(false);
            if (existingCategory != null)
                throw new EntityAlreadyExistsException(category);
        }
    }
}
