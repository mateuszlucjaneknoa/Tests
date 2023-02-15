using Test1.Domain.ExternalApi;
using Test1.Domain.Pricing;

namespace Test1.ComponentTests.TestSuite.Mocks
{
    internal class RiskFetcherMock : IRiskFetcher
    {
        public RiskFetcherMock()
        {
            foreach (var risk in DefaultValues.RiskFetcher.GetDefaultValues())
            {
                AddRisk(risk.positionId, risk.risk);
            }
        }

        Dictionary<int, Risk> riskMap = new Dictionary<int, Risk>();

        public void AddRisk(int positionId, Risk risk)
        {
            riskMap[positionId] = risk;
        }

        public Risk Fetch(int positionId, DateTimeOffset time)
        {
            if (!riskMap.ContainsKey(positionId))
            {
                throw new Exception($"no risk mock for id {positionId}");
            }
            return riskMap[positionId];
        }
    }
}
