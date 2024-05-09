using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ProductWebAPI.Models
{
    public class AuthOptions
    {
        public const string ISSUER = "MyATBServer"; // издатель токена
        public const string AUDIENCE = "MyATB"; // потребитель токена
        const string KEY = "onemorebaby_onemoretime_onemorebaby_onemoretime!";   // ключ для шифрацииS
        public static SymmetricSecurityKey GetSymmetricSecurityKey() => 
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
