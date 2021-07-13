using HubBlogAssignment.UI.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using System;
using System.Threading.Tasks;

namespace HubBlogAssignment.UI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //HttpClient for things that require Auth. E.g. Creating comments on posts.
            builder.Services.AddHttpClient("HubBlog.Api.Auth", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ApiUrl"]);
            }).AddHttpMessageHandler(sp => sp.GetRequiredService<AuthorizationMessageHandler>()
              .ConfigureHandler(new[] { builder.Configuration["ApiUrl"] }, new[] { builder.Configuration["AzureAdB2C:Scope"] }));

            //HttpClient for things that don't require Auth. E.g. Viewing Posts.
            builder.Services.AddHttpClient("HubBlog.Api.NoAuth", client => 
            {
                client.BaseAddress = new Uri(builder.Configuration["ApiUrl"]); 
            });

            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
                options.ProviderOptions.DefaultAccessTokenScopes.Add(builder.Configuration["AzureAdB2C:Scope"]);
            });

            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddMudServices();

            await builder.Build().RunAsync();
        }
    }
}
