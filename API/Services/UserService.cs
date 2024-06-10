using API.Models;
using API.Models.Contexts;
using System.Security.Cryptography;
using System.Text;

namespace API.Services
{
    public class UserService 
    {
        private readonly DatabaseContext _context;
        public UserService(DatabaseContext context) 
        {
            _context = context;
        }

        public async Task<bool> Register(Profile profile)
        {
            profile.Password = HashPassword(profile.Password);
            if (_context.Profiles.Any(p => p.Email == profile.Email))
            {
                return false;
            }
            _context.Profiles.Add(profile);
            _context.SaveChanges();
            return true;
        }

        public async Task<(bool,int)> Login(string email, string password)
        {
            password = HashPassword(password);
            var profile = _context.Profiles.FirstOrDefault(p => p.Email == email && p.Password == password);
            if (profile == null)
            {
                return (false,0);
            }
            return (true, profile.Id);
        }


        public string HashPassword(string password)
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
    }
}
