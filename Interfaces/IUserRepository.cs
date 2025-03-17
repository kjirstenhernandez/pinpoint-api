using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.User;
using api.Models;

namespace api.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(string id);

        Task<User> CreateAsync(User userModel);
        Task<User?> UpdateAsync(string id, UpdateUserRequestDto userDto);

        Task<User?> DeleteAsync(string id);
        Task<bool> UserExists(string id);
    }
}