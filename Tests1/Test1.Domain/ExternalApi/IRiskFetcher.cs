using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test1.Domain.Pricing;

namespace Test1.Domain.ExternalApi
{
    public interface IRiskFetcher
    {
        Risk Fetch(int positionId, DateTimeOffset time);
    }
}
