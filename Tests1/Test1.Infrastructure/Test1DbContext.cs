using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Transactions;
using Test1.Domain.Base;
using Test1.Domain.ExternalApi;
using Test1.Domain.Order;
using Test1.Infrastructure.Confifuration;

namespace Test1.Infrastructure
{
    public class Test1DbContext : DbContext, IRepository<Order>, IRepository<Person>, ITransaction
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Person> Customers { get; set; }

        private Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction? transaction = null;

        public Test1DbContext(DbContextOptions<Test1DbContext> options) : base(options)
        {
        }
        protected Test1DbContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new OrderConfiguration().Configure(modelBuilder.Entity<Order>());
            new PersonConfiguration().Configure(modelBuilder.Entity<Person>());
        }

        public async Task<ITransaction> OpenTransaction()
        {
            if (Database.IsSqlServer())
                transaction = Database.BeginTransaction();
            return this;
        }

        public async Task<ITransaction> CommitTransaction()
        {
            await SaveChangesAsync();

            if (transaction != null)
            {
                await transaction.CommitAsync();
            }

            return this;
        }

        public async Task<Person> Put(Person element)
        {
            return (Person)await Put(element, typeof(Person));
        }

        public async Task<Entity?> Get(int key)
        {
            return await FindAsync(typeof(Person), key) as Person;
        }

        public async Task<Order> Put(Order element)
        {
            return (Order)await Put(element, typeof(Order));
        }

        private async Task<Entity> Put(Entity element, Type type)
        {
            var elementInDb = await FindAsync(type, element.Id);
            if (elementInDb != null)
            {
                if (!ReferenceEquals(elementInDb, element))
                {
                    throw new ArgumentException($"when updating element with given ID work on its copy. ID: {element.Id}");
                }
                Entry(elementInDb).State = EntityState.Modified;
            }
            else
            {
                await AddAsync(element);
            }

            return element;
        }
    }
}