using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using HubBlogAssignment.Data;
using Microsoft.EntityFrameworkCore;
using HubBlogAssignment.Api.Filters;
using HubBlogAssignment.Data.DataAccess;
using HubBlogAssignment.Data.Interfaces;

namespace HubBlogAssignment.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAdB2C"));

            services.AddCors(opts =>
            {
                opts.AddPolicy("CorsPolicy", policy => 
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            services.AddTransient<IPostAccess, PostAccess>();
            services.AddTransient<ICommentAccess, CommentAccess>();
            services.AddTransient<ICategoryAccess, CategoryAccess>();
            services.AddTransient<IUserAccess, UserAccess>();
            services.AddAutoMapper(typeof(Program));
            services.AddDbContext<HubDbContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString("Hub")));

            services.AddScoped<NewUserFilter>();
            services.AddControllers(opts => opts.Filters.Add(typeof(NewUserFilter)));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HubBlogAssignment.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HubBlogAssignment.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
