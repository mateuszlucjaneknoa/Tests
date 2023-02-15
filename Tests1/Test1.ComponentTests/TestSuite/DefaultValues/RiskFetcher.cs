using Test1.Domain.Pricing;

namespace Test1.ComponentTests.TestSuite.DefaultValues
{
    internal class RiskFetcher
    {
        public static IEnumerable<(int positionId, Risk risk)> GetDefaultValues() => new[]
        {
            (1, new Risk(2,3,5,7, RiskBracket.Medium))
        };
    }
}
