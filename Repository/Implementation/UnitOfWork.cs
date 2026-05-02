using Microsoft.EntityFrameworkCore.Storage;
using Repopattern.Data;
using Repopattern.Model;
using Repopattern.Repository.Interface;
using System;

namespace Repopattern.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LearnDBContext _context;

        public IGenericRepository<Product> Products { get; }
        private IDbContextTransaction _transaction;

        public UnitOfWork(LearnDBContext context)
        {
            _context = context;
            Products = new GenericRepository<Product>(context);
        }

        public async Task<int> CompleteAsync()
            => await _context.SaveChangesAsync();

        // 🔹 Begin Transaction
        public async Task BeginTransactionAsync()
        {
            if (_transaction != null) return;

            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }
        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
