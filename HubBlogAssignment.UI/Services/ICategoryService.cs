using System.Collections.Generic;
using System.Threading.Tasks;

namespace HubBlogAssignment.UI.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<string>> GetCategories();
    }
}
