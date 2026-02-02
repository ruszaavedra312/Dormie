using Dormie.Models;

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
    }
}
