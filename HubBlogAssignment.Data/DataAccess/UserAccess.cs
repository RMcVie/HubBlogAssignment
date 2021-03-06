using HubBlogAssignment.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using HubBlogAssignment.Data.Entities.Database;

namespace HubBlogAssignment.Data.DataAccess
{
    public class UserAccess : IUserAccess
    {
        private readonly HubDbContext context;

        public UserAccess(HubDbContext context)
        {
            this.context = context;
        }
        public async Task EnsureUserExists(Guid objectId, string displayName)
        {
            var existingUser = await context.Set<User>().SingleOrDefaultAsync(u => u.AadObjectId == objectId).ConfigureAwait(false);
            if (existingUser != null)
                return;
            context.Set<User>().Add(new User { AadObjectId = objectId, DisplayName = displayName });
            await context.SaveChangesAsync();
        }
    }
}
