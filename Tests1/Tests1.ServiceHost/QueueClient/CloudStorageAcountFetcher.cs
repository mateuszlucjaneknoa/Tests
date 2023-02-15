using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests1.ServiceHost.QueueClient
{
    public interface ICloudQueueFetcher
    {
        Task<CloudQueue> GetCloudQueue(string connectionString, string queueName);
    }

    public class CloudStorageAcountFetcher : ICloudQueueFetcher
    {
        public async Task<CloudQueue> GetCloudQueue(string connectionString, string queueName)
        {
            var queue= CloudStorageAccount.Parse(connectionString).CreateCloudQueueClient().GetQueueReference(queueName);

            await queue.CreateIfNotExistsAsync();
            return queue;
        }
    }
}
