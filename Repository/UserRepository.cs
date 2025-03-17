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
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User userModel)
        {
            await _context.Users.AddAsync(userModel);
            await _context.SaveChangesAsync();
            return userModel;
        }

        public async Task<User?> DeleteAsync(string id)
        {
            var userModel = await _context.Users.FirstOrDefaultAsync(x => x.id == id);
            if(userModel == null){
                return null;
            }
            _context.Users.Remove(userModel);
            await _context.SaveChangesAsync();
            return userModel;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.Include(c => c.Events).ToListAsync();
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            return await _context.Users.Include(c => c.Events).FirstOrDefaultAsync(i => i.id == id);
        }

        public async Task<User?> UpdateAsync(string id, UpdateUserRequestDto userDto)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.id == id);
            if (existingUser == null){
                return null;
            }

                existingUser.username = userDto.username;
                existingUser.firstName = userDto.firstName;
                existingUser.lastName = userDto.lastName;
                existingUser.zipcode = userDto.zipcode;
                existingUser.email = userDto.email;
                existingUser.phone = userDto.phone;

                await _context.SaveChangesAsync();
                return existingUser;
        }

        public Task<bool> UserExists(string id)
        {
            return _context.Users.AnyAsync(s => s.id == id);
        }
    }
}