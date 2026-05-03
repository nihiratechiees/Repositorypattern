using Repopattern.Model;

namespace Repopattern.Repository.Interface
{
    public interface IOrderItemRepository
    {
        Task Save(List<Orderitem> entity);
    }
}
