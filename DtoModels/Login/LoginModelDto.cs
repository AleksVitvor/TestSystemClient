using System.ComponentModel.DataAnnotations;

namespace DtoModels.Login
{
    public class LoginModelDto
    {
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Password should be between 4 and 50 characters")]
        public string Password { get; set; }

        [Required]
        public string Username { get; set; }
    }
}
