using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1.Infrastructure
{
    public class DesignTimeFactory : IDesignTimeDbContextFactory<Test1DbContext>
    {
        public Test1DbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Test1DbContext>();
            optionsBuilder.UseSqlServer("Server=IGNPLKRAW10L011;Database=test;Trusted_Connection=True;");

            return new Test1DbContext(optionsBuilder.Options);
        }
    }
}
