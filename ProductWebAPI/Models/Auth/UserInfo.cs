namespace ProductWebAPI.Models
{
    public class UserInfo
    {
        public static List<User> Users { get; } = new List<User>
        {
            new User
            {
                Id = 1,
                FirstName = "Іван",
                LastName = "Ковальов",
                Email = "ivankoval@example.com",
                DateOfBirth = new DateTime(1850, 11, 11),
                Password = "ivankoval2004",
                LastLoginDate = new DateTime(2024, 3, 20),
                FailedLoginAttempts = 5
            },
        };
    }
}
