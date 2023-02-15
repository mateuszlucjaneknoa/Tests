using Microsoft.Extensions.Hosting;
using Tests1.ServiceHost;
using Tests1.ServiceHost.QueueClient;

CatalogGenerationHost _job;
var host = Startup.GetHostBuilder().Build();
_job = (CatalogGenerationHost)host.Services.GetService(typeof(CatalogGenerationHost));
await host.StartAsync();
