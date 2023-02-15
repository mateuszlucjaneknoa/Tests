using Microsoft.EntityFrameworkCore;
using Test1.Application;
using Test1.Domain.ExternalApi;
using Test1.Domain.Order;
using Test1.Domain.Pricing;
using Test1.Infrastructure;

namespace Test1.ServiceHost.Web
{
    public static class Startup
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            var services = builder.Services;
            var configuration = builder.Configuration;
            services.AddTransient<ISpreadCalculator, SpreadCalculator>();
            services.AddTransient<IRiskFetcher, DummyRiskFetcher>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IOrderFacade, OrderFacade>();
            RegisterInfrastructure(services, configuration);
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
