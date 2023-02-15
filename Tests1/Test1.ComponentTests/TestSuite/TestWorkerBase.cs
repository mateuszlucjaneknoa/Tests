using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Test1.ComponentTests.TestSuite.DefaultValues;
using Test1.Infrastructure;

namespace Test1.ComponentTests.TestSuite
{
    public abstract class TestWorkerBase
    {
        protected abstract IHost _host { get; }

        public TestWorkerBase()
        { }

        protected void Register(IServiceCollection services)
        {
            services.ConfigureBasicMocks();
            ReplaceOverride(services);
        }

        protected abstract void ReplaceOverride(IServiceCollection services);

        internal T GetInstance<T>() => _host.Services.CreateScope().ServiceProvider.GetRequiredService<T>();
        internal object GetInstance(Type t) => _host.Services.CreateScope().ServiceProvider.GetService(t);

        public Test1DbContext DbContext => _host.Services.CreateScope().ServiceProvider.GetRequiredService<Test1DbContext>();

        protected void AddDefaultMocks()
        {
            var dbContext = DbContext;
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            foreach (var person in PersonCollection.GetPerson())
            {
                dbContext.Customers.Add(person);
            }

            dbContext.SaveChanges();
        }
    }
}
