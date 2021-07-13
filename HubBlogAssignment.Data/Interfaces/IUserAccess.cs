using System;
using System.Threading.Tasks;

namespace HubBlogAssignment.Data.Interfaces
{
    public interface IUserAccess
    {
        Task EnsureUserExists(Guid objectId, string displayName);
    }
}
