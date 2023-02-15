using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1.Domain.Pricing;
public static class RiskTable
{
    public static decimal GetRiskMultiplier(this RiskBracket bracket)
    {
        return bracket switch
        {
            RiskBracket.Unset => throw new NotSupportedException("risk must be set"),
            RiskBracket.High => 1.5m,
            RiskBracket.Low => 0.5m,
            RiskBracket.Medium => 1m
        };
    }
}
