using ProductWebAPI.Models;

namespace ProductWebAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly List<User> _users;

        public AuthService()
        {
            _users = UserInfo.Users;
        }

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            var user = _users.SingleOrDefault(u => u.Email == email && u.Password == password);
            return user;
        }

        public async Task<User> RegisterAsync(User newUser)
        {
            if (_users.Any(u => u.Email == newUser.Email))
            {
                throw new InvalidOperationException("Користувач з цією адресою електронної пошти вже існує.");
            }

            _users.Add(newUser);
            return newUser;
        }
    }
}
