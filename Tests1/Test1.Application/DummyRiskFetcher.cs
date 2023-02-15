using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test1.Domain.ExternalApi;
using Test1.Domain.Pricing;

namespace Test1.Application
{
    public class DummyRiskFetcher : IRiskFetcher
    {
        public Risk Fetch(int positionId, DateTimeOffset time)
        {
            throw new NotImplementedException();
        }
    }
}
