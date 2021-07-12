﻿using HubBlogAssignment.Api.ExtensionMethods;
using HubBlogAssignment.Data.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


// I would not do this under normal cirumstances.
// User creation should be it's own isolated process (Utilizing Azure B2C Custom Policies).
// But for ease of a demo, I'll do it like this!
// Plus hey, at least it shows I know how to use action filters!

namespace HubBlogAssignment.Api.Filters
{
    public class NewUserFilter : IAsyncActionFilter
    {
        private readonly IUserAccess userAccess;

        public NewUserFilter(IUserAccess userAccess)
        {
            this.userAccess = userAccess;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
                await userAccess.EnsureUserExists(context.HttpContext.User.GetAadObjectId(), context.HttpContext.User.GetDisplayName()).ConfigureAwait(false);
            await next();
        }
    }
}