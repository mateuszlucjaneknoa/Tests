using Test1.Domain.Pricing;

namespace Test1.UnitTests.Examples.Examples
{
    internal class DummyClass
    {
        private readonly ISpreadCalculator spreadCalculator;

        public DummyClass(ISpreadCalculator spreadCalculator)
        {
            this.spreadCalculator = spreadCalculator;
        }

        public Spread Calculate(Risk risk)
        {
            var amount = (int)risk.NominalPrice;
            return spreadCalculator.GetSpread(risk, amount);
        }
    }
}
