using Repopattern.Data;
using Repopattern.Model;
using Repopattern.Repository.Interface;

namespace Repopattern.Repository.Implementation
{
    public class OrderRepository:IOrderRepository
    {
        private readonly LearnDBContext _context;
        public OrderRepository(LearnDBContext context)
        {
            _context = context;
        }
        public async Task Save(Order entity)
        {
           await _context.Orders.AddAsync(entity);
        }

    }
}
