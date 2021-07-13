using HubBlogAssignment.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HubBlogAssignment.Data
{
    public interface ICategoryAccess
    {
        Task<IEnumerable<Category>> GetCategories();
        Task CreateCategory(Category category);
    }
}
