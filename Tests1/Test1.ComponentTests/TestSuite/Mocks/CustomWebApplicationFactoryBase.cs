using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Test1.ComponentTests.TestSuite.Mocks
{
    public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
    {
        private Action<IServiceCollection> register;
        private IHost _host;

        public CustomWebApplicationFactory(Action<IServiceCollection> register)
        {
            this.register = register;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                register(services);
            });

            builder.UseEnvironment("Development");
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            _host = base.CreateHost(builder);
            return _host;
        }

        internal IHost? GetHost()
        {
            return _host;
        }
    }
}