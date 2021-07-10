using HubBlogAssignment.UI.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HubBlogAssignment.UI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped<CustomAuthorizationMessageHandler>();
            builder.Services.AddHttpClient("ServerAPI", client =>
              client.BaseAddress = new Uri("https://personalazurefunction.azurewebsites.net/api/"))
                    .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:7071/api/")});
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
             .CreateClient("ServerAPI"));
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddMudServices();

            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
                options.ProviderOptions.DefaultAccessTokenScopes.Add("https://personalazurefunction.azurewebsites.net/api/");
            });

            await builder.Build().RunAsync();
        }
    }
}
