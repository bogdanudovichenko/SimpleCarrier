using SimpleCarrier.Domain.Entities.Users;
using SimpleCarrier.Domain.RepositoryInterfaces.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleCarrier.Domain.RepositoryInterfaces.Users
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<bool> IsInRoleAsync(int id, string role);
        Task<User> FindByUserNameAsync(string userName);

        Task<IEnumerable<Role>> GetRolesAsync(int id);
        Task AddToRoleAsync(int id, string role);
        Task RemoveFromRoleAsync(int id, string role);

        Task<bool> CheckPasswordAsync(User user, string password);
    }
}
