using System.ComponentModel.DataAnnotations;
namespace shorter.client.Data.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [Display(Name = "username")]
        public required string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "email address")]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "password")]
        public required string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "passwords don't match.")]
        [Display(Name = "confirm password")]
        public required string ConfirmPassword { get; set; }
    }
}