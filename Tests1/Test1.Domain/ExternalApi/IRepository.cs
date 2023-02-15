using Test1.Domain.Base;

namespace Test1.Domain.ExternalApi
{
    public interface IRepository<T> where T : Entity
    {

        Task<T> Put(T element);

        Task<Entity?> Get(int key);
    }

    public interface ITransaction : IDisposable
    {
        Task<ITransaction> OpenTransaction();

        Task<ITransaction> CommitTransaction();
    }
}
