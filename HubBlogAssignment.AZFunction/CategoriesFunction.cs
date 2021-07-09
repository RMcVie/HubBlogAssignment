using HubBlogAssignment.Data;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HubBlogAssignment.AZFunction
{
    public class CategoriesFunction
    {
        private readonly IDataAccess dataAccess;

        public CategoriesFunction(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        [Function("GetCategories")]
        public async Task<HttpResponseData> Get([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Categories")] HttpRequestData req)
        {
            var categories = await dataAccess.GetCategories().ConfigureAwait(false);
            var response = req.CreateResponse();

            await response.WriteAsJsonAsync(categories).ConfigureAwait(false);

            return response;
        }

        [Function("CreateCategory")]
        public async Task<HttpResponseData> Post([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Categories")] HttpRequestData req)
        {
            var category = await req.ReadFromJsonAsync<string>().ConfigureAwait(false);

            await dataAccess.CreateCategory(category).ConfigureAwait(false);
            return req.CreateResponse(HttpStatusCode.OK);
        }
    }
}
