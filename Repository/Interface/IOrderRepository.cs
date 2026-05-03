using Repopattern.Model;

namespace Repopattern.Repository.Interface
{
    public interface IOrderRepository
    {
        Task Save(Order entity);
    }
}
