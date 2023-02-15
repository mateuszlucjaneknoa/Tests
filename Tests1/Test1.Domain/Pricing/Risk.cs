namespace Test1.Domain.Pricing
{
    public record Risk(decimal NominalPrice, double StockPriceVariation, decimal CustomerMargin, double CustomerMarginIndex, RiskBracket Bracket)
    {

        /// <summary>
        /// Total risk of given trade
        /// </summary>
        public double TotalRiskFactor => GetTotalRisk();

        private double GetTotalRisk()
        {
            var multiplier = Bracket.GetRiskMultiplier();
            return 1 - (Math.Min((double)CustomerMargin, StockPriceVariation * 100) * (double)multiplier) / ((double)CustomerMargin * CustomerMarginIndex);
        }
    }
}