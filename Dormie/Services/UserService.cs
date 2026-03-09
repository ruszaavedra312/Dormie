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
            // 🔹 Check if email already exists
            if (await _repo.EmailExistsAsync(user.Email))
                throw new Exception("Email has been taken");

            // 🔹 Hash password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = hashedPassword;

            // 🔹 Save
            await _repo.AddUserAsync(user);
        }
    }
}
