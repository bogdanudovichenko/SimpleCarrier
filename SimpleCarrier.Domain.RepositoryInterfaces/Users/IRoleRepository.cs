using SimpleCarrier.Domain.Entities.Users;
using SimpleCarrier.Domain.RepositoryInterfaces.Base;
using System.Threading.Tasks;

namespace SimpleCarrier.Domain.RepositoryInterfaces.Users
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Task<Role> FindByNameAsync(string name);
    }
}
