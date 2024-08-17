using Microsoft.EntityFrameworkCore;
using TunifyPlatform.Data;
using TunifyPlatform.Models;
using TunifyPlatform.Repositories.interfaces;

namespace TunifyPlatform.Repositories.Services
{
    public class UserService : IUser
    {
        private readonly TunifyDbContext _context;

        public UserService(TunifyDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            var allUsers = await _context.Users.ToListAsync();
            return allUsers;
        }

        public async Task<User> GetByIdAsync(int UserId) => await _context.Users.FindAsync(UserId);

        public async Task<User> InsertAsync(User User)
        {
            _context.Users.Add(User);
            await _context.SaveChangesAsync();
            return User;
        }

        public async Task<User> UpdateAsync(int id, User User)
        {
            var exsitingEmployee = await _context.Users.FindAsync(id);
            exsitingEmployee = User;
            await _context.SaveChangesAsync();
            return User;
        }

        public async Task<User> DeleteAsync(int id)
        {
            var User = await GetByIdAsync(id);
            _context.Entry(User).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return User;
        }
    }
}
