using System.ComponentModel.DataAnnotations;
namespace ProductWebAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ім'я є обов'язковим")]
        [StringLength(15, ErrorMessage = "Максимальна довжина імені - 15 символів")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Прізвище є обов'язковим")]
        [StringLength(15, ErrorMessage = "Максимальна довжина прізвища - 15 символів")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Адреса електронної пошти є обов'язковою")]
        [EmailAddress(ErrorMessage = "Неправильний формат адреси електронної пошти")]
        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Пароль є обов'язковим")]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "Мінімальна довжина пароля - 8 символів")]
        public string Password { get; set; }

        public DateTime LastLoginDate { get; set; }

        public int FailedLoginAttempts { get; set; }
    }
}
