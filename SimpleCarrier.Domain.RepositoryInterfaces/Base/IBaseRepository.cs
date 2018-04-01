using SimpleCarrier.Common.GetItems;
using SimpleCarrier.Domain.Entities.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleCarrier.Domain.RepositoryInterfaces.Base
{
    public interface IBaseRepository<T> where T : DomainEntity
    {
        Task<T> FindByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<ItemsWithTotalCountModel<T>> Get(GetItemsModel getItemsModel);
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(int id);
    }
}