using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Reflection;
using Test1.ComponentTests.TestSuite.Mocks;
using Tests1.ServiceHost;
using Tests1.ServiceHost.QueueClient;

namespace Test1.ComponentTests.TestSuite
{
    public class ServiceBusTestWorker : TestWorkerBase
    {
        internal Dictionary<string, (Type type, MethodInfo method)> registeredListeners = new Dictionary<string, (Type type, MethodInfo method)>();

        protected override IHost _host { get; }
        private MockQueue mockQueue = new MockQueue();

        public ServiceBusTestWorker() : base()
        {
            var hb =Startup.GetHostBuilder().ConfigureServices(Register);
            _host = hb.Build();

            AddDefaultMocks();
            RegisterListener("Order", typeof(OrderListener));
        }

        public async Task ScheduleMessage<T>(T message, string queueName)
        {
            var jobHost = GetInstance<IJobHost>();

            var cloudMessage = new CloudQueueMessage(System.Text.Json.JsonSerializer.Serialize(message));

            await jobHost.CallAsync(queueName, new Dictionary<string, object>() { { "message", cloudMessage } });
        }

        protected override void ReplaceOverride(IServiceCollection services)
        {
            MockAzure(services);
        }

        internal T GetMessageOfType<T>()
        {
            var client = mockQueue;
            return client.GetMessagesOfType<T>().First();
        }

        internal T GetService<T>() where T : notnull
        {
            return _host.Services.CreateScope().ServiceProvider.GetRequiredService<T>();
        }


        private void RegisterListener(string name, Type type)
        {

            var methods = type.GetMethods();

            var methodInfo = methods.Where(x => x.GetParameters().Any(y => y.ParameterType == typeof(CloudQueueMessage))).First();
            registeredListeners.Add(name, (type, methodInfo));
        }

        private void MockAzure(IServiceCollection services)
        {
            services.Remove(services.First(x => x.ServiceType == typeof(IJobHost)));

            services.AddSingleton<IJobHost>(new MockJobHost(this));

            services.Remove(services.First(x => x.ServiceType == typeof(ICloudQueueFetcher)));
            services.AddSingleton<ICloudQueueFetcher, MockQueueFetcher>(x => new MockQueueFetcher(mockQueue));

        }
    }
}
