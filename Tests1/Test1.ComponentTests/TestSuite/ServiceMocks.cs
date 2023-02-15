using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Test1.ComponentTests.TestSuite.Mocks;
using Test1.Domain.ExternalApi;
using Test1.Infrastructure;

namespace Test1.ComponentTests.TestSuite
{
    internal static class ServiceRegistryMocks
    {
        public static void ConfigureBasicMocks(this IServiceCollection services)
        {
            MockDatabase(services);
            RemoveHostedServices(services);

            services.Remove(services.First(x => x.ServiceType == typeof(IRiskFetcher)));
            services.AddTransient<IRiskFetcher, RiskFetcherMock>();
        }
        private static void RemoveHostedServices(IServiceCollection services)
        {
            var hostedServices = services.Where(x => x.ServiceType == typeof(IHostedService)).ToList();
            foreach (var hostedService in hostedServices)
            {
                services.Remove(hostedService);
            }
        }

        private static void MockDatabase(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<Test1DbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            var optionsBuilder = new DbContextOptionsBuilder<Test1DbContext>();
            optionsBuilder.UseInMemoryDatabase("DatabaseName");

            services.AddSingleton(optionsBuilder.Options);
        }
    }
}
