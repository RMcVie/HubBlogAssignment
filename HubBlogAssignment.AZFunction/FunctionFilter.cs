using Microsoft.Azure.WebJobs.Host;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HubBlogAssignment.AZFunction
{
    public class FunctionAuthorizeAttribute : FunctionInvocationFilterAttribute
    {
        public FunctionAuthorizeAttribute()
        {
        }

        public override Task OnExecutingAsync(FunctionExecutingContext executingContext, CancellationToken cancellationToken)
        {
            var workItem = executingContext.Arguments.First().Value as HttpRequestMessage;
            var jwtInput = workItem.Headers.Authorization;
            var handler = new JwtSecurityTokenHandler();
            var objectId = GetObjectIdClaim(jwtInput.ToString().Substring("Bearer ".Length).Trim(), handler);            
            return base.OnExecutingAsync(executingContext, cancellationToken);
        }
        public static string GetObjectIdClaim (string jwt, JwtSecurityTokenHandler handler)
        {
            var token = handler.ReadJwtToken(jwt);
            return token.Claims.Single(x => x.Type == "sub").Value;            
        }

    }
}
