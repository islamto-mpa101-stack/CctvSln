using System.ComponentModel.DataAnnotations;

namespace Cctv.Views.User
{
    public class RegisterVm
    {
        [Required, MaxLength(255), MinLength(3)]
        public string UserName { get; set; }
        [Required, MaxLength(255), MinLength(3), EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(255), MinLength(3), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, MaxLength(255), MinLength(3), DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}
