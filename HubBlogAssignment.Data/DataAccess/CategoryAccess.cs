using HubBlogAssignment.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HubBlogAssignment.Data
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
            context.Set<Category>().Add(category);
            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await context.Set<Category>().ToListAsync();
        }
    }
}
