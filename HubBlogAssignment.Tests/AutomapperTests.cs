using AutoMapper;
using HubBlogAssignment.Api;
using Xunit;

namespace HubBlogAssignment.Tests
{
    public class AutoMapperTests
    {
        [Fact]
        public void AutoMapperSetupIsValid()
        {
            var mapperConfig = new MapperConfiguration(m => m.AddProfile(new EntityMapperProfile()));
            mapperConfig.AssertConfigurationIsValid();
        }
    }
}
