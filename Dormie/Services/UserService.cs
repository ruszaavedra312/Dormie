using Dormie.Models;
using Dormie.Data;

namespace Dormie.Services
{
    public class UserService
    {
        private readonly UserRepository _repo;

        public UserService(UserRepository repo)
        {
            _repo = repo;
        }

        public async Task CreateUserAsync(User user)
        {
            // 1️⃣ Validate
            if (string.IsNullOrWhiteSpace(user.Password))
                throw new Exception("Password is required");

            if (user.Password.Length < 6)
                throw new Exception("Password must be at least 6 characters");

            // 2️⃣ Hash password
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            // 3️⃣ Clear raw password (important)
            user.Password = null;

            // 4️⃣ Save
            await _repo.AddUserAsync(user);
        }
    }
}
