namespace Test1.Domain.Pricing
{
    public class SpreadCalculator : ISpreadCalculator
    {
        public Spread GetSpread(Risk risk, decimal amount)
        {
            var spread = risk.NominalPrice * 0.1m * (decimal)risk.TotalRiskFactor * amount;
            var halfSpread = spread / 2;
            return new Spread(risk.NominalPrice, risk.NominalPrice - halfSpread, risk.NominalPrice + halfSpread, halfSpread);
        }
    }
}