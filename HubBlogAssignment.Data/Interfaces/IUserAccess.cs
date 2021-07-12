using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubBlogAssignment.Data.Interfaces
{
    public interface IUserAccess
    {
        Task EnsureUserExists(Guid objectId, string displayName);
    }
}
