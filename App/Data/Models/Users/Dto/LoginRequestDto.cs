using System.ComponentModel.DataAnnotations;

namespace Restaurant.App.Data.Models.Users.Dto;

public class LoginRequestDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}
