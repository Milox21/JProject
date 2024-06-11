using MobileTeaApp.Models;
using System.Security.Cryptography;
using System.Text;

namespace MobileTeaApp.Services
{
    public class AuthService
    {
        public int Id { get; set; }
        public bool IsLoggedIn { get; set; }
        public ApiConnectionService<Profile> _apiConnectionService { get; set; }


        public AuthService(ApiConnectionService<Profile> apiConnectionService)
        {
            _apiConnectionService = apiConnectionService;
            IsLoggedIn = false;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public async Task<bool> Register(Profile profile)
        {
            profile.Password = HashPassword(profile.Password);
            bool success = await _apiConnectionService.Register(profile);
            return (success);
        }

        public async Task<bool> Login(string email, string password)
        {
            Profile profile = new()
            {
                Email = email,
                Password = HashPassword(password)
            };

            var (success, userId) = await _apiConnectionService.Login(profile);

            Id = userId;
            if(success)
            {
                IsLoggedIn = true;
            }
            return (success);
        }   
    }
}
