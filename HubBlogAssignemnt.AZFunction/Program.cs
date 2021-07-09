using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using HubBlogAssignemnt.Data;
using AutoMapper;

namespace HubBlogAssignemnt.AZFunction
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(serviceProvider=>
                {
                    serviceProvider.AddSingleton<IDataAccess, MockDataAccess>();
                    serviceProvider.AddAutoMapper(typeof(Program));
                })
                .Build();

            host.Run();
        }
    }
}