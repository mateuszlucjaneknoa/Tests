using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using Test1.Domain.Pricing;
using Test1.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Test1.Domain.ExternalApi;
using Test1.Domain.Order;
using Test1.Application;
using Tests1.ServiceHost.QueueClient;
using Microsoft.Azure.WebJobs.Host.Executors;

namespace Tests1.ServiceHost
{
    public static class Startup
    {
        public static IHostBuilder GetHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices(ConfigureServices)
                .ConfigureWebJobs(b =>
                {
                    b.AddServiceBus();
                });
        }

        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            var configuration = context.Configuration;
            services.AddTransient<ISpreadCalculator, SpreadCalculator>();
            services.AddTransient<IRiskFetcher, DummyRiskFetcher>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IOrderFacade, OrderFacade>();
            RegisterInfrastructure(services, configuration);
            services.AddTransient<IQueueClient, QueueClient.QueueClient>(x => new QueueClient.QueueClient("", "", (ICloudQueueFetcher)x.GetService(typeof(ICloudQueueFetcher))));
            services.AddTransient<ICloudQueueFetcher, CloudStorageAcountFetcher>();
            services.AddTransient<OrderListener>();
            services.AddSingleton<CatalogGenerationHost>();
        }

        private static void RegisterInfrastructure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Test1DbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

            services.AddScoped<IRepository<Order>>(x => (IRepository<Order>)x.GetService(typeof(Test1DbContext)));
            services.AddScoped<IRepository<Person>>(x => (IRepository<Person>)x.GetService(typeof(Test1DbContext)));
            services.AddScoped<ITransaction>(x => (ITransaction)x.GetService(typeof(Test1DbContext)));
        }

    }
}