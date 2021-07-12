using HubBlogAssignment.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubBlogAssignment.Data
{
    public interface ICategoryAccess
    {
        Task<IEnumerable<Category>> GetCategories();
        Task CreateCategory(Category category);
    }
}
