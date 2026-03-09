using Dormie.Data;
using Dormie.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Dormie.Services
{
    public class AuthService
    {
        private readonly DormieDbContext _context;
        private readonly IHttpContextAccessor _http;

        public AuthService(DormieDbContext context, IHttpContextAccessor http)
        {
            _context = context;
            _http = http;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
                return false;

            bool valid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            if (!valid)
                return false;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserType.ToString())
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            var principal = new ClaimsPrincipal(identity);

            await _http.HttpContext!.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal
            );

            return true;
        }

        public async Task LogoutAsync()
        {
            await _http.HttpContext!.SignOutAsync();
        }
    }
}