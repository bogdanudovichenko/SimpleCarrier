using Dapper;
using FastMapper.NetCore;
using SimpleCarrier.Common.GetItems;
using SimpleCarrier.Domain.Entities.Users;
using SimpleCarrier.Domain.RepositoryInterfaces.Users;
using SimpleCarrier.Infrastructure.Repositories.DbModels;
using SimpleCarrier.Infrastructure.Repositories.Postgres.Dapper.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCarrier.Infrastructure.Repositories.Postgres.Dapper.Users
{
    public class PostgresDapperUserRepository : PostgreDapperBaseRepository, IUserRepository
    {
        private readonly IRoleRepository _roleRepository;

        public PostgresDapperUserRepository(string connection, IRoleRepository roleRepository) : this(connection)
        {
            _roleRepository = roleRepository;
        }

        public PostgresDapperUserRepository(string connection) : base(connection)
        {
        }

        public async Task AddToRoleAsync(Int32 id, String role)
        {
            User findedUser = await FindByIdAsync(id);
            if (findedUser == null) return;

            Role findedRole = await _roleRepository.FindByNameAsync(role);
            if (findedRole == null) return;

            findedUser.Roles.Add(findedRole);
            await UpdateAsync(findedUser);
        }

        public async Task CreateAsync(User item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            var userDbModel = TypeAdapter.Adapt<User, UserDbModel>(item);

            string query = $@"INSERT INTO {_usersTableName} 
                              (
                               {nameof(UserDbModel.Id)}, 
                               {nameof(UserDbModel.UserName)}
                              ) 
                              VALUES
                              (
                               @{nameof(UserDbModel.Id)}, 
                               @{nameof(UserDbModel.UserName)}
                              )";

            using (IDbConnection db = OpenedConnection)
            {
                IDbTransaction transaction = db.BeginTransaction();

                var userProfileDbModel = TypeAdapter.Adapt<UserProfile, UserProfileDbModel>(item.UserProfile);

                string userProfileQuery = $@"INSERT INTO {_userProfilesTable}
                                                  ({nameof(UserProfileDbModel.FirstName)}, {nameof(UserProfileDbModel.LastName)})
                                            VALUES(@{nameof(UserProfileDbModel.FirstName)}, @{nameof(UserProfileDbModel.LastName)})";

                int createdUserProfileId = await db.QuerySingleAsync<int>(userProfileQuery, userProfileDbModel);

                userDbModel.UserProfileId = createdUserProfileId;
                await db.ExecuteAsync(query, userDbModel);
                transaction.Commit();
            }
        }

        public async Task DeleteAsync(Int32 id)
        {
            User findedUser = await FindByIdAsync(id);
            if (findedUser == null) return;

            using (IDbConnection db = OpenedConnection)
            {
                await db.ExecuteAsync($"DELETE FROM {_usersTableName} WHERE id=@id", new { id });
            }
        }

        public async Task<User> FindByIdAsync(Int32 id)
        {
            using (IDbConnection db = OpenedConnection)
            {
                var findedUserDbModel = await db.QuerySingleAsync<UserDbModel>($"SELECT * FROM {_usersTableName} WHERE WHERE id=@id", new { id });
                var findedUser = TypeAdapter.Adapt<UserDbModel, User>(findedUserDbModel);

                return findedUser;
            }
        }

        public async Task<User> FindByUserNameAsync(String userName)
        {
            if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException(nameof(userName));

            string query = $"SELECT * FROM { _usersTableName} WHERE WHERE {nameof(UserDbModel.UserName)} = @{nameof(UserDbModel.UserName)}";

            using (IDbConnection db = OpenedConnection)
            {
                var findedUserDbModel = await db.QuerySingleAsync<UserDbModel>(query, new UserDbModel { UserName = userName });
                var findedUser = TypeAdapter.Adapt<UserDbModel, User>(findedUserDbModel);

                return findedUser;
            }
        }

        public Task<ItemsWithTotalCountModel<User>> Get(GetItemsModel getItemsModel)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using (IDbConnection db = OpenedConnection)
            {
                var usersFromDb = await db.QueryAsync<UserDbModel>($"SELECT * FROM {_usersTableName}");
                var users = TypeAdapter.Adapt<IEnumerable<UserDbModel>, IEnumerable<User>>(usersFromDb);
                return users;
            }
        }

        public async Task<IEnumerable<Role>> GetRolesAsync(Int32 id)
        {
            //string query = $@"SELECT * 
            //                  FROM {_rolesTableName} 
            //                  INNER JOIN {_userRolesTableName} ON {_rolesTableName}.id = {nameof(RoleDbModel.RoleId)}
            //                  INNER JOIN {_usersTableName} ON {_userRolesTableName}.{nameof(RoleDbModel.UserId)} = {_usersTableName}.id
            //                  WHERE {_usersTableName}.id = @id";

            throw new NotImplementedException();

            //using (IDbConnection db = OpenedConnection)
            //{
            //    var rolesFromDb = await db.QueryAsync<RoleDbModel>(query, new { id });
            //    var roles = TypeAdapter.Adapt<IEnumerable<RoleDbModel>, IEnumerable<Role>>(rolesFromDb);
            //    return roles;
            //}
        }

        public async Task<bool> IsInRoleAsync(Int32 id, string role)
        {
            if (string.IsNullOrEmpty(role)) throw new ArgumentNullException(nameof(role));

            IEnumerable<Role> roles = await GetRolesAsync(id);
            bool result = roles.Any(r => r.Name.ToLower() == role.ToLower());
            return result;
        }

        public async Task RemoveFromRoleAsync(Int32 id, String role)
        {
            throw new NotImplementedException();

            //Role roleModel = await _roleRepository.FindByNameAsync(role);
            //if (role == null) return;

            //string query = $@"DELETE FROM {_userRolesTableName}
            //                  WHERE {nameof(RoleDbModel.UserId)} = @id AND {nameof(RoleDbModel.RoleId)} = @roleId";

            //using (IDbConnection db = OpenedConnection)
            //{
            //    await db.ExecuteAsync(query, new { id, roleId = roleModel.Id });
            //}
        }
        
        public Task UpdateAsync(User item)
        {
            throw new NotImplementedException();
        }

        public Task<Boolean> CheckPasswordAsync(User user, String password)
        {
            throw new NotImplementedException();
        }
    }
}
