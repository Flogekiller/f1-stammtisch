using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class RegisterUserViewModel
{
    [Required(ErrorMessage = "The name field is required")]
    [EmailAddress(ErrorMessage = "The email field is invalid")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "The password field is required")]
    [StringLength(100, ErrorMessage = "The password must be at least 6 characters long", MinimumLength = 6)]
    public required string Password { get; set; }

    [Compare("Password", ErrorMessage = "The passwords do not match")]
    public required string ConfirmPassword { get; set; }
}