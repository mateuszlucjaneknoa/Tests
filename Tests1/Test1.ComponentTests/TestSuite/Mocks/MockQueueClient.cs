using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace Test1.ComponentTests.TestSuite.Mocks
{
    public class MockQueue : CloudQueue
    {
        private List<CloudQueueMessage> _orders = new List<CloudQueueMessage>();

        public MockQueue() : base(new StorageUri(new Uri("http://noa.pl")), null)
        {
        }

        public IEnumerable<T> GetMessagesOfType<T>()
        {
            var messages = new List<T>();
            foreach (var message in _orders)
            {
                try
                {
                    var typedMessage = System.Text.Json.JsonSerializer.Deserialize<T>(message.AsString);
                    if (!typedMessage?.Equals(default) ?? false)
                        messages.Add(typedMessage);
                }
                catch (Exception)
                { }
            }
            return messages;
        }

        public override Task AddMessageAsync(CloudQueueMessage message)
        {
            _orders.Add(message);
            return Task.CompletedTask;
        }
    }
}