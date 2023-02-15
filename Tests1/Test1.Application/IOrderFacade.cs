using Test1.Domain.Order;
using static Test1.Application.OrderFacade;

namespace Test1.Application
{
    public interface IOrderFacade
    {
        Task<Order> PlaceOrder(OrderRequest request);
    }
}