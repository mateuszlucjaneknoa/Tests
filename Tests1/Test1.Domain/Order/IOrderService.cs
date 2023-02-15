namespace Test1.Domain.Order
{
    public interface IOrderService
    {
        Task<Order> PlaceOrder(int ownerId, int positionId, decimal amount, DateTimeOffset timeSignature);
    }
}