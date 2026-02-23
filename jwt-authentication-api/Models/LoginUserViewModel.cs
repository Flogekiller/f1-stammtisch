using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class LoginUserViewModel
{
    [Required(ErrorMessage = "The email field is required")]
    [EmailAddress(ErrorMessage = "The email field is invalid")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "The password field is required")]
    public required string Password { get; set; }
}