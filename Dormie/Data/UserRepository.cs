using Dormie.Models;
using Microsoft.EntityFrameworkCore;

namespace Dormie.Data
{
    public class UserRepository
    {
        private readonly DormieDbContext _context;

        public UserRepository(DormieDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        // Optional but good practice
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users
                .AnyAsync(u => u.Email == email);
        }
    }
}
