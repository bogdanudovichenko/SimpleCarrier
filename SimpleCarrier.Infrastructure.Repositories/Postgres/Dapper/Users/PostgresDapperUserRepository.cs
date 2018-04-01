using Dapper;
using SimpleCarrier.Common.GetItems;
using SimpleCarrier.Domain.Entities.Users;
using SimpleCarrier.Domain.RepositoryInterfaces.Base;
using SimpleCarrier.Domain.RepositoryInterfaces.Users;
using SimpleCarrier.Infrastructure.Repositories.Postgres.Dapper.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SimpleCarrier.Infrastructure.Repositories.Postgres.Dapper.Users
{
    public class PostgresDapperUserRepository : PostgreDapperBaseRepository, IUserRepository
    {
        public PostgresDapperUserRepository(String connection) : base(connection)
        {
        }

        public Task AddToRoleAsync(Int32 id, String role)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(User item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Int32 id)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByIdAsync(Int32 id)
        {
            throw new NotImplementedException();

            //using(IDbConnection db = OpenedConnection)
            //{
            //    db.Query<User>()
            //}
        }

        public Boolean FindByUserNameAsync(String userName)
        {
            throw new NotImplementedException();
        }

        public Task<ItemsWithTotalCountModel<User>> Get(GetItemsModel getItemsModel)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetRolesAsync(Int32 id)
        {
            throw new NotImplementedException();
        }

        public Boolean IsInRoleAsync(Int32 id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(Int32 id, String role)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User item)
        {
            throw new NotImplementedException();
        }
    }
}
