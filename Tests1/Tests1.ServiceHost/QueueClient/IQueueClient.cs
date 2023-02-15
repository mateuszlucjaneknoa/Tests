using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Test1.Domain.Order;

namespace Tests1.ServiceHost.QueueClient
{
    public interface IQueueClient
    {
        Task SendMessage<T>(T message);
    }
    internal class QueueClient : IQueueClient
    {
        private readonly CloudQueue _queue;

        public QueueClient(string connectionString, string queueName, ICloudQueueFetcher fetcher)
        {
            _queue = fetcher.GetCloudQueue(connectionString, queueName).GetAwaiter().GetResult();
        }

        public async Task SendMessage<T>(T message)
        {
            var cloudMessage = new CloudQueueMessage(System.Text.Json.JsonSerializer.Serialize(message));

            await _queue.AddMessageAsync(cloudMessage);
        }
    }
}
