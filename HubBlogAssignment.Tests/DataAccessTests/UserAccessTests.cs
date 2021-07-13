using System;
using System.Threading.Tasks;
using FluentAssertions;
using HubBlogAssignment.Data;
using HubBlogAssignment.Data.DataAccess;
using HubBlogAssignment.Data.Entities.Database;
using HubBlogAssignment.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HubBlogAssignment.Tests.DataAccessTests
{
    public class UserAccessTests : BaseDataAccessTest
    {
        private readonly IUserAccess userAccess;

        public UserAccessTests()
        {
            userAccess = new UserAccess(new HubDbContext(DbContextOptions));
        }

        [Fact]
        public async Task NewUserIsCreated()
        {
            const string newDisplayName = "New User";
            var newUserObjectId = new Guid();

            await userAccess.EnsureUserExists(newUserObjectId, newDisplayName).ConfigureAwait(false);

            await using var context = new HubDbContext(DbContextOptions);
            var insertedUser = context.Set<User>()
                .SingleOrDefaultAsync(u => u.DisplayName == newDisplayName && u.AadObjectId == newUserObjectId)
                .ConfigureAwait(false);
            insertedUser.Should().NotBeNull();
        }

        [Fact]
        public async Task ExistingUserIsUnaffected()
        {
            User existingUser;
            User potentiallyChangedUser;
            await using (var context = new HubDbContext(DbContextOptions))
            {
                existingUser = await context.Set<User>().FirstAsync().ConfigureAwait(false);
            }

            await userAccess.EnsureUserExists(existingUser.AadObjectId, "");

            await using (var context = new HubDbContext(DbContextOptions))
            {
                potentiallyChangedUser = await context.Set<User>()
                    .SingleOrDefaultAsync(u => u.AadObjectId == existingUser.AadObjectId).ConfigureAwait(false);
            }

            potentiallyChangedUser.Should().NotBeNull();
            potentiallyChangedUser.Should().BeEquivalentTo(existingUser);
        }
    }
}
