using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Test1.Domain.Order;
using Tests1.ServiceHost.QueueClient;

namespace Test1.ComponentTests.TestSuite.Mocks
{
    internal class MockQueueFetcher : ICloudQueueFetcher
    {
        private MockQueue mockQueue;

        public MockQueueFetcher(MockQueue mockQueue)
        {
            this.mockQueue = mockQueue;
        }

        public Task<CloudQueue> GetCloudQueue(string connectionString, string queueName)
        {
            return Task.FromResult(mockQueue as CloudQueue);
        }
    }
}