using MobileTeaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MobileTeaApp.Services
{
    public class AuthService
    {
        public int Id { get; set; }
        public bool IsLoggedIn { get; set; }
        public ApiConnectionService<Profile> _apiConnectionService { get; set; }


        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }


        public AuthService(ApiConnectionService<Profile> apiConnectionService)
        {
            _apiConnectionService = apiConnectionService;
            IsLoggedIn = false;
        }

        public async Task<bool> Register(Profile profile)
        {
            bool success = await _apiConnectionService.Register(profile);
            return (success);
        }

        public async Task<bool> Login(string email, string password)
        {
            Profile profile = new()
            {
                Email = email,
                //Password = HashPassword(password)
                Password = password
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
