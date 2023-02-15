namespace Test1.Domain.Pricing
{
    public record Spread(decimal NominalPrice, decimal SellPrice, decimal BuyPrice, decimal HalfSpread)
    { }
}