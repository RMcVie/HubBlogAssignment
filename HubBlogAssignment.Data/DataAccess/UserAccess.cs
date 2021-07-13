using HubBlogAssignment.Data.Entities;
using HubBlogAssignment.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

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
            var existingUser = await context.Set<User>().SingleOrDefaultAsync(u => u.AADObjectId == objectId).ConfigureAwait(false);
            if (existingUser != null)
                return;
            context.Set<User>().Add(new User { AADObjectId = objectId, DisplayName = displayName });
            await context.SaveChangesAsync();
        }
    }
}
