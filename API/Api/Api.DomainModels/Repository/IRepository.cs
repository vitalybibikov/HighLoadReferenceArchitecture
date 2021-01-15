using System.Threading.Tasks;

namespace Api.DomainModels.Repository
{
    public interface IRepository<T>
    {
        Task CreateAsync(T c);

        Task UpsertAsync(T c);

        Task<bool> ExistsAsync(T c);

        Task<T> GetAsync(string id);
    }
}
