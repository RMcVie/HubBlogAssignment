using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HubBlogAssignment.UI.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<string>> GetCategories();
    }
}
