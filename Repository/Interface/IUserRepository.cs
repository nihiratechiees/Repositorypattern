using Repopattern.Model;

namespace Repopattern.Repository.Interface
{
    public interface IUserRepository
    {
        Task<User?> GetUser(string username, string password);

        Task AddUser(User user);
    }
}
