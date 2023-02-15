using Microsoft.Azure.WebJobs;

namespace Tests1.ServiceHost.QueueClient
{
    public class CatalogGenerationHost : IDisposable
    {
        private readonly IJobHost _host;

        public CatalogGenerationHost(IJobHost host)
        {
            _host = host;
            _host.StartAsync(new CancellationToken()).GetAwaiter().GetResult();
        }

        public void Dispose()
        {
            _host.StopAsync().GetAwaiter().GetResult();
        }
    }
}