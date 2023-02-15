using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Test1.ComponentTests.TestSuite.Mocks;

namespace Test1.ComponentTests.TestSuite
{
    internal class HttpTestWorker : TestWorkerBase
    {
        protected override IHost _host { get; }
        private HttpClient _httpClient;
        public HttpClient HttpClient => _httpClient;

        public HttpTestWorker()
        {
            var factory = new CustomWebApplicationFactory<Program>(Register);
            _httpClient = factory.CreateClient();
            _host = factory.GetHost();

            AddDefaultMocks();
        }

        protected override void ReplaceOverride(IServiceCollection services)
        { }
    }
}
