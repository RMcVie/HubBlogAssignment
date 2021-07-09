using AutoMapper;
using HubBlogAssignemnt.AZFunction;
using System;
using Xunit;

namespace HubBlogAssignment.Tests
{
    public class AutmapperTests
    {
        [Fact]
        public void AutoMapperSetupIsValid()
        {
            var mapperConfig = new MapperConfiguration(m => m.AddProfile(new EntityMapperProfile()));
            mapperConfig.AssertConfigurationIsValid();
        }
    }
}