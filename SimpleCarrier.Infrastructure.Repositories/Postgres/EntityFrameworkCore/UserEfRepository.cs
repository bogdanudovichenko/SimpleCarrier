using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleCarrier.Common.GetItems;
using SimpleCarrier.Domain.Entities.Users;
using SimpleCarrier.Domain.RepositoryInterfaces.Users;
using SimpleCarrier.Infrastructure.Repositories.DbModels;

namespace SimpleCarrier.Infrastructure.Repositories.Postgres.EntityFrameworkCore
{
    public class UserEfRepository : IUserRepository
    {
        private readonly SimpleCarrierDbContext _db;
        private readonly UserManager<UserDbModel> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserEfRepository(SimpleCarrierDbContext db, UserManager<UserDbModel> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task AddToRoleAsync(int id, string role)
        {
            UserDbModel user = await _userManager.FindByIdAsync(id.ToString());
            if(user != null) await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<bool> CheckPasswordAsync(User user, System.String password)
        {
            var userDbModel = Mapper.Map<User, UserDbModel>(user);
            return await _userManager.CheckPasswordAsync(userDbModel, password);
        }

        public async Task CreateAsync(User item)
        {
            var userDbModel = Mapper.Map<User, UserDbModel>(item);
            userDbModel.Email = item.UserName;           

            if(item.UserProfile != null)
            {
                userDbModel.UserProfile = Mapper.Map<UserProfile, UserProfileDbModel>(item.UserProfile);
            }

            IdentityResult identityResult = await _userManager.CreateAsync(userDbModel);

            if (!identityResult.Succeeded) return;

            await _userManager.AddToRolesAsync(userDbModel, item.Roles.Select(r => r.Name).ToList());
        }

        public async Task DeleteAsync(int id)
        {
            UserDbModel userDbModel = await _userManager.FindByIdAsync(id.ToString());
            if (userDbModel != null) await _userManager.DeleteAsync(userDbModel);
        }

        public async Task<User> FindByIdAsync(int id)
        {
            UserDbModel userDbModel = await _userManager.FindByIdAsync(id.ToString());
            var user = Mapper.Map<UserDbModel, User>(userDbModel);

            IEnumerable<string> roleNames = await _userManager.GetRolesAsync(userDbModel);

            var rolesTasks = new List<Task<IdentityRole>>();

            foreach(string roleName in roleNames)
            {
                Task<IdentityRole> roleTask = _roleManager.FindByNameAsync(roleName);
                rolesTasks.Add(roleTask);
            }

            Task.WaitAll(rolesTasks.ToArray());

            foreach(Task<IdentityRole> roleTask in rolesTasks)
            {
                IdentityRole identityRole = roleTask.Result;
                var role =  Mapper.Map<IdentityRole, Role>(identityRole);
                user.Roles.Add(role);
            }

            return user;
        }

        public async Task<User> FindByUserNameAsync(string userName)
        {
            UserDbModel userDbModel = await _userManager.FindByNameAsync(userName);
            var user = Mapper.Map<UserDbModel, User>(userDbModel);

            IEnumerable<string> roleNames = await _userManager.GetRolesAsync(userDbModel);

            var rolesTasks = new List<Task<IdentityRole>>();

            foreach (string roleName in roleNames)
            {
                Task<IdentityRole> roleTask = _roleManager.FindByNameAsync(roleName);
                rolesTasks.Add(roleTask);
            }

            Task.WaitAll(rolesTasks.ToArray());

            foreach (Task<IdentityRole> roleTask in rolesTasks)
            {
                IdentityRole identityRole = roleTask.Result;
                var role = Mapper.Map<IdentityRole, Role>(identityRole);
                user.Roles.Add(role);
            }

            return user;
        }

        public Task<ItemsWithTotalCountModel<User>> Get(GetItemsModel getItemsModel)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            IEnumerable<UserDbModel> userDbModels = await _db.Users.ToListAsync();
            var users = Mapper.Map<IEnumerable<UserDbModel>, IEnumerable<User>>(userDbModels);
            return users;
        }

        public Task<IEnumerable<Role>> GetRolesAsync(System.Int32 id)
        {
            throw new System.NotImplementedException();
        }

        public Task<System.Boolean> IsInRoleAsync(System.Int32 id, System.String role)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveFromRoleAsync(System.Int32 id, System.String role)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(User item)
        {
            throw new System.NotImplementedException();
        }
    }
}
