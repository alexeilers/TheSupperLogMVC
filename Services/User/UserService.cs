using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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


        //CREATE USER
        public async Task<bool> CreateUserAsync(UserCreate model)
        {
            var userEntity = new UserEntity
            {
                Email = model.Email,
                Username = model.Username,
                Password = model.Password,
                DateAdded = DateTime.Now,
            };

            _context.Users.Add(userEntity);
            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }


        //DELETE USER
        public async Task<bool> DeleteUserAsync(int userId)
        {
            var userEntity = await _context.Users.FindAsync(userId);

            _context.Users.Remove(userEntity);
            return await _context.SaveChangesAsync() == 1; ;
        }

        public async Task<IEnumerable<UserListItem>> GetAllUsersAsync()
        {
            var userQuery = _context
                .Users
                .Select(m =>
                new UserListItem
                {
                    Id = m.Id,
                    Email = m.Email,
                    Username = m.Username,
                    DateAdded = m.DateAdded
                });
            return await userQuery.ToListAsync();
        }


        //UPDATE USER
        public async Task<bool> UpdateUserAsync(UserEdit model)
        {
            var user = await _context.Users.FindAsync(model.Id);

            user.Username = model.Username;
            user.Email = model.Email;
            user.DateAdded = model.DateAdded;

            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }


        //GET USER BY ID
        public async Task<UserDetail> GetUserByIdAsync(int userId)
        {
            var model = await _context.Users.FirstOrDefaultAsync(m => m.Id == userId);

            if (model == null) return null;

            var user = new UserDetail
            {
                Username = model.Username,
                Email = model.Email,
                DateAdded = model.DateAdded,
            };

            return user;
        }
    }
}
