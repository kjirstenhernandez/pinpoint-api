using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.User;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;  // Database connection

        public UserRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User userModel) // Use the information provided in teh request body to create a new user
        {
            await _context.Users.AddAsync(userModel); // Creating user and pending
            await _context.SaveChangesAsync(); // Saving the pending user
            return userModel;
        }

        public async Task<User?> DeleteAsync(string id) // Delete a user using ID to select
        {
            var userModel = await _context.Users.FirstOrDefaultAsync(x => x.id == id); // find the user by ID
            if(userModel == null){ 
                return null; // if no user is found, return null
            }
            _context.Users.Remove(userModel); // Delete the user's instance, pending
            await _context.SaveChangesAsync(); // Save the changes to the database
            return userModel;
        }

        public async Task<List<User>> GetAllAsync() // Gather the information for all users
        {
            return await _context.Users.Include(c => c.Events).ToListAsync(); 
        }

        public async Task<User?> GetByIdAsync(string id) // Find a user by their Unique ID
        {
            return await _context.Users.Include(c => c.Events).FirstOrDefaultAsync(i => i.id == id);
            // this returns both the user information as well as an array of any events they may have created
        }

        public async Task<User?> UpdateAsync(string id, UpdateUserRequestDto userDto) // Update the selected user's information with the info provided in the request body
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.id == id); // Find the user by ID
            if (existingUser == null){
                return null; // if no user exists, return null
            }
                // Replace the existing user information with the information provided in the request body. 
                existingUser.username = userDto.username;
                existingUser.firstName = userDto.firstName;
                existingUser.lastName = userDto.lastName;
                existingUser.zipcode = userDto.zipcode;
                existingUser.email = userDto.email;
                existingUser.phone = userDto.phone;

                await _context.SaveChangesAsync(); // Save the changes to the database
                return existingUser; // return the user reflecting the changes
        }

        public Task<bool> UserExists(string id)
        {
            // searches the database for the user; saves time on validation later. 
            return _context.Users.AnyAsync(s => s.id == id);
        }
    }
}