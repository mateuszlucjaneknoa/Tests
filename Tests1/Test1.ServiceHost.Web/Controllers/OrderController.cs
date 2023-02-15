using Microsoft.AspNetCore.Mvc;
using static Test1.Application.OrderFacade;
using Test1.Application;
using Test1.Domain.Order;

namespace Test1.ServiceHost.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderFacade orderFacade;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderFacade orderFacade, ILogger<OrderController> logger)
        {
            _logger = logger;
            this.orderFacade = orderFacade;
        }

        [HttpPost(Name = "PlaceOrder")]
        public async Task<Order> Post(OrderRequest request)
        {
            return await orderFacade.PlaceOrder(request);
        }
    }
}