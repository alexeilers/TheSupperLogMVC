using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheSupperLog.Models.User;

namespace TheSupperLog.Services.User
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(UserCreate model);
        //Task<IEnumerable<UserListItem>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(int userId);
    }
}
