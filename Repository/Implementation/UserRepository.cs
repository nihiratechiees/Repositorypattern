using Microsoft.EntityFrameworkCore;
using Repopattern.Data;
using Repopattern.Model;
using Repopattern.Repository.Interface;

namespace Repopattern.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly LearnDBContext _context;
        public UserRepository(LearnDBContext learnDBContext)
        {
            this._context = learnDBContext;

        }
       public async Task<User?> GetUser(string username, string password)
        {
           return await _context.user.FirstOrDefaultAsync(o=>o.Username == username && o.Password == password);
        }

        public async Task AddUser(User user)
        {
           _context.user.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
