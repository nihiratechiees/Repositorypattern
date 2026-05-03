using Repopattern.Model;

namespace Repopattern.Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository Orders { get; }
        IOrderItemRepository OrderItems { get; }

        Task<int> CompleteAsync(); // SaveChanges
    }
}
