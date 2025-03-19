using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.User;
using api.Models;

namespace api.Interfaces
{
    public interface IUserRepository
    // User Repo Interface
    {
        Task<List<User>> GetAllAsync(); // Get a list of all users
        Task<User?> GetByIdAsync(string id); // Displays a user and their list of events

        Task<User> CreateAsync(User userModel); // create a new user
        Task<User?> UpdateAsync(string id, UpdateUserRequestDto userDto); // update a user's information

        Task<User?> DeleteAsync(string id); // Delete a specified user
        Task<bool> UserExists(string id); // verify that a user exists in the DB using their unique ID
    }
}