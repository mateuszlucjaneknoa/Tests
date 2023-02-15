using Test1.Domain.Base;

namespace Test1.Domain.Order
{
    public class Order : Entity
    { 
        public decimal Amount { get; set; }

        public int PositionId { get; set; }

        public int PlacerId { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTimeOffset OrderTime { get; set; }
    }
}