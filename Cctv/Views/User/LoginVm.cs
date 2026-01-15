using System.ComponentModel.DataAnnotations;

namespace Cctv.Views.User
{
    public class LoginVm
    {
        [Required, MaxLength(255), MinLength(3), EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(255), MinLength(3), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
