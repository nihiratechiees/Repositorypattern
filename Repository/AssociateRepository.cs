using Microsoft.EntityFrameworkCore;
using Repopattern.Data;
using Repopattern.Model;

namespace Repopattern.Repository
{
    public class AssociateRepository : IAssociateRepository
    {
        private readonly LearnDBContext _context;
        public AssociateRepository(LearnDBContext learnDBContext)
        {
            _context = learnDBContext;
        }
        public async Task Create(Associate entity)
        {
            _context.associates.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Boolean> Delete(string id)
        {
            var _product = await _context.associates.FindAsync(id);
            if (_product!=null)
            {
                _context.associates.Remove(_product);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Associate?> Get(string id)
        {
            return await _context.associates.FindAsync(id);
        }

        public async Task<IEnumerable<Associate>> GetAll()
        {
            return await _context.associates.ToListAsync();
        }

        public async Task Update(Associate entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
