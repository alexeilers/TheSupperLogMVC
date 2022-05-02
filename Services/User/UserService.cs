using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheSupperLog.Data;
using TheSupperLog.Data.Entities;
using TheSupperLog.Models.User;

namespace TheSupperLog.Services.User
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateUserAsync(UserCreate model)
        {
            var userEntity = new UserEntity
            {
                Email = model.Email,
                Username = model.Username
            };

            _context.Users.Add(userEntity);
            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var userEntity = await _context.Users.FindAsync(userId);

            _context.Users.Remove(userEntity);
            return await _context.SaveChangesAsync() == 1; ;
        }

        //public Task<IEnumerable<UserListItem>> GetAllUsersAsync()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
