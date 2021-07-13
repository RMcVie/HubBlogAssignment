using System;
using System.Linq;
using System.Security.Claims;

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
