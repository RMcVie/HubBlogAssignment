using System.Collections.Generic;
using System.Threading.Tasks;
using HubBlogAssignment.Data.Entities;
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
            context.Set<Category>().Add(category);
            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await context.Set<Category>().ToListAsync();
        }
    }
}
