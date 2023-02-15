namespace Test1.Domain.Pricing
{
    public interface ISpreadCalculator
    {
        Spread GetSpread(Risk risk, decimal amount);
    }
}