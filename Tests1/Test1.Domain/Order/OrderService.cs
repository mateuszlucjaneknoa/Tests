using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test1.Domain.ExternalApi;
using Test1.Domain.Pricing;

namespace Test1.Domain.Order
{
    public class OrderService : IDisposable, IOrderService
    {
        private readonly IRiskFetcher _riskFetcher;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Person> _personRepository;
        private readonly ITransaction _transaction;
        private readonly ISpreadCalculator _spreadCalculator;

        public OrderService(
            IRiskFetcher riskFetcher,
            IRepository<Order> orderRepository,
            IRepository<Person> personRepository,
            ITransaction transaction,
            ISpreadCalculator spreadCalculator)
        {
            _riskFetcher = riskFetcher;
            _orderRepository = orderRepository;
            _personRepository = personRepository;
            _transaction = transaction;
            _spreadCalculator = spreadCalculator;
        }

        public async Task<Order> PlaceOrder(int ownerId, int positionId, decimal amount, DateTimeOffset timeSignature)
        {
            var risk = _riskFetcher.Fetch(positionId, timeSignature);
            var spread = _spreadCalculator.GetSpread(risk, amount);
            await _transaction.OpenTransaction();

            var owner = await _personRepository.Get(ownerId);

            if (owner == null)
            {
                throw new ArgumentNullException($"There is no person with id {ownerId}.");
            }

            var order = new Order
            {
                Amount = amount,
                OrderTime = timeSignature,
                PlacerId = ownerId,
                PositionId = positionId,
                TotalPrice = spread.BuyPrice * amount
            };

            order = await _orderRepository.Put(order);

            await _transaction.CommitTransaction();

            return order;
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }
    }
}
