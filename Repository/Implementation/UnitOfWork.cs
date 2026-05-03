using Microsoft.EntityFrameworkCore.Storage;
using Repopattern.Data;
using Repopattern.Model;
using Repopattern.Repository.Interface;
using System;

namespace Repopattern.Repository.Implementation
{
    public class UnitOfWork(
        LearnDBContext context,
        IOrderRepository orderRepository,
        IOrderItemRepository orderItem) : IUnitOfWork
    {
        private readonly LearnDBContext _context = context;
        public IOrderRepository Orders { get; } = orderRepository;
        public IOrderItemRepository OrderItems { get; } = orderItem;
        private IDbContextTransaction? _transaction;

        public async Task<int> CompleteAsync()
            => await _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
