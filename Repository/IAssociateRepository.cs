using Repopattern.Model;

namespace Repopattern.Repository
{
    public interface IAssociateRepository
    {
        Task<IEnumerable<Associate>> GetAll();
        Task<Associate?> Get(string id);

        Task Create(Associate entity);
        Task Update(Associate entity);
        Task<Boolean> Delete(string id);

    }
}
