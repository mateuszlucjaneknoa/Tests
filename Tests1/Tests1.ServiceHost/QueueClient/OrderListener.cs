using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using Test1.Application;
using static Test1.Application.OrderFacade;

namespace Tests1.ServiceHost.QueueClient
{
    public class OrderListener
    {
        private readonly IOrderFacade orderFacade;
        private readonly IQueueClient queueClient;

        public OrderListener(IOrderFacade orderFacade, IQueueClient queueClient)
        {
            this.orderFacade = orderFacade;
            this.queueClient = queueClient;
        }

        public async Task ProcessQueueMessage([QueueTrigger("Orders")] CloudQueueMessage message)
        {
            OrderRequest catalogMsg = JsonConvert.DeserializeObject<OrderRequest>(message.AsString);

            await queueClient.SendMessage(await orderFacade.PlaceOrder(catalogMsg));
        }
    }
}