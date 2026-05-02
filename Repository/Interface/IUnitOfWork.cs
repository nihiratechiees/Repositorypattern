using Repopattern.Model;

namespace Repopattern.Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Product> Products { get; }

        Task<int> CompleteAsync(); // SaveChanges
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
