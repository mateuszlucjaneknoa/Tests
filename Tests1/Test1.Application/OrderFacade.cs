using Test1.Domain.Order;

namespace Test1.Application
{
    public class OrderFacade : IOrderFacade
    {
        private readonly IOrderService orderService;

        public OrderFacade(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public record OrderRequest(int ownerId, int positionId, decimal amount) { }
        public async Task<Order> PlaceOrder(OrderRequest request)
        {
            return await orderService.PlaceOrder(request.ownerId, request.positionId, request.amount, DateTimeOffset.UtcNow);
        }
    }
}