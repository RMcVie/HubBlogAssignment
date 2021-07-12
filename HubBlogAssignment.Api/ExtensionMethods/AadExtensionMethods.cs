using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HubBlogAssignment.Api.ExtensionMethods
{
    public static class AadExtensionMethods
    {
        public static Guid GetAadObjectId(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = claimsPrincipal.Claims.Single(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier");
            return new Guid(claim.Value);
        }

        public static string GetDisplayName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.Single(c => c.Type == "name").Value;
        }
    }
}
