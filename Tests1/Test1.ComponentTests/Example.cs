using FluentAssertions;
using System.Net.Http.Json;
using System.Text.Json;
using Test1.Application;
using Test1.ComponentTests.TestSuite;
using Test1.Domain.ExternalApi;
using Test1.Domain.Order;
using Xunit;
using static Test1.Application.OrderFacade;

namespace Test1.ComponentTests
{
    public class Example
    {
        [Fact]
        public async Task SampleTest()
        {
            var suite = new ServiceBusTestWorker();
            await suite.ScheduleMessage(new OrderRequest(1, 1, 3.3m), "Order");
            var order = suite.GetMessageOfType<Order>();
            ValidateResult(suite, order);
        }

        [Fact]
        public async Task SampleTestButHttp()
        {
            var suite = new HttpTestWorker();
            var response = await suite.HttpClient.PostAsJsonAsync("/order", new OrderRequest(1, 1, 3.3m));
            var body = await response.Content.ReadAsStringAsync();
            var order = System.Text.Json.JsonSerializer.Deserialize<Order>(body, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            ValidateResult(suite, order);
        }

        private void ValidateResult(TestWorkerBase suite, Order order)
        {
            var expected = new Order
            {
                Amount = 3.3m,
                Id = 1,
                PlacerId = 1,
                PositionId = 1,
                TotalPrice = 7.53m
            };

            var dbContext = suite.DbContext;
            var dbOrder = dbContext.Orders.Single();

            order.Should().BeEquivalentTo(dbOrder, options => options.ComparingByMembers<Order>());

            order.Should().BeEquivalentTo(expected, options =>
            {
                options.Excluding(y => y.TotalPrice);
                options.Excluding(y => y.OrderTime);
                return options;
            });

            ((float)order.TotalPrice).Should().BeApproximately((float)expected.TotalPrice, 0.01F);
        }
    }
}