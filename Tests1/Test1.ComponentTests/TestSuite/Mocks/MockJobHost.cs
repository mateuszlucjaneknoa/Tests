using Castle.Core.Logging;
using Microsoft.Azure.WebJobs;

namespace Test1.ComponentTests.TestSuite.Mocks
{
    internal class MockJobHost : IJobHost, IDisposable
    {
        private readonly ServiceBusTestWorker worker;

        public MockJobHost(ServiceBusTestWorker worker)
        {
            this.worker = worker;
        }

        public async Task CallAsync(string name, IDictionary<string, object> arguments = null, CancellationToken cancellationToken = default)
        {
            if (!worker.registeredListeners.ContainsKey(name))
            {
                throw new Exception("listener not registered");
            }

            var executionData = worker.registeredListeners[name];

            var instance = worker.GetInstance(executionData.type);

            if (executionData.method.GetParameters().Count() == 2 && executionData.method.GetParameters().Last().ParameterType == typeof(ILogger))
            {
                //mocked ilogger instance
                var result = executionData.method.Invoke(instance, new[] { arguments["message"], null });
                if (result != null && result is Task t)
                    await t;
            }
            else
            {
                var result =executionData.method.Invoke(instance, new[] { arguments["message"] });
                if (result != null && result is Task t)
                    await t;
            }
        }

        public void Dispose()
        {
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            return Task.CompletedTask;
        }
    }
}
