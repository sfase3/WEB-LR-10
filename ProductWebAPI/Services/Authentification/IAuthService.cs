using System.Threading.Tasks;
using ProductWebAPI.Models;

namespace ProductWebAPI.Services
{
    public interface IAuthService
    {
        Task<User> AuthenticateAsync(string email, string password);
        Task<User> RegisterAsync(User user);
    }
}
