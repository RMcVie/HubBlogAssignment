using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using HubBlogAssignment.Data;
using HubBlogAssignment.Data.DataAccess;
using HubBlogAssignment.Data.Entities.Database;
using HubBlogAssignment.Data.Errors;
using HubBlogAssignment.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HubBlogAssignment.Tests.DataAccessTests
{
    public class CategoryDataAccessTests : BaseDataAccessTest
    {
        private readonly ICategoryAccess dataAccess;
        public CategoryDataAccessTests()
        {
            dataAccess = new CategoryAccess(new HubDbContext(DbContextOptions));
        }

        [Fact]
        public async Task AllCategoriesAreReturned()
        {
            var categories = await dataAccess.GetCategories().ConfigureAwait(false);
            categories.Count().Should().Be(2);
            categories.Should().Contain(c => c.Name == "Finance");
            categories.Should().Contain(c => c.Name == "Tech");
        }

        [Fact]
        public async Task NewCategoryIsCreated()
        {
            var newCategory = new Category { Name = "New Category"};
            await dataAccess.CreateCategory(newCategory).ConfigureAwait(false);

            await using var context = new HubDbContext(DbContextOptions);
            var categories = await context.Set<Category>().ToListAsync().ConfigureAwait(false);
            categories.Should().Contain(c => c.Name == "New Category");
        }

        [Fact]
        public void ErrorIsThrownIfInsertingCategoryThatAlreadyExists()
        {
            var existingCategory = new Category { Name = "Finance" };
            Func<Task> action = async () => await dataAccess.CreateCategory(existingCategory).ConfigureAwait(false);

            action.Should().Throw<EntityAlreadyExistsException>().WithMessage("Entity of Type HubBlogAssignment.Data.Entities.Database.Category Already Exists!");
        }
    }
}
