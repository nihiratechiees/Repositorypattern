using Repopattern.Data;
using Repopattern.Model;
using Repopattern.Repository.Interface;

namespace Repopattern.Repository.Implementation
{
    public class OrderItemRepository: IOrderItemRepository
    {
        private readonly LearnDBContext _context;
        public OrderItemRepository(LearnDBContext context)
        {
            _context = context;
        }

        public async Task Save(List<Orderitem> entity)
        {
            await _context.Orderitems.AddRangeAsync(entity);
        }
    }
}
