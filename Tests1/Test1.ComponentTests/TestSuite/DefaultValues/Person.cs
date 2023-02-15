using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test1.Domain.Order;

namespace Test1.ComponentTests.TestSuite.DefaultValues
{
    internal class PersonCollection
    {
        public static IEnumerable<Person> GetPerson() => new[]
        {
            new Person()
            {
                Salutation = "Mr",
                BirthDate = new DateTime(2000,01,01),
                FirstName = "ShouldICallYou",
                LastName = "Mister?",
            }
        };
    }
}
