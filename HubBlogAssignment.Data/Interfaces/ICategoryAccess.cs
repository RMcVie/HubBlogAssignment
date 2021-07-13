using System.Collections.Generic;
using System.Threading.Tasks;
using HubBlogAssignment.Data.Entities;

namespace HubBlogAssignment.Data.Interfaces
{
    public interface ICategoryAccess
    {
        Task<IEnumerable<Category>> GetCategories();
        Task CreateCategory(Category category);
    }
}
