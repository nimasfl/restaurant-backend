namespace Restaurant.App.Data.Models.Users.Dto;

public class LoginResponseDto
{
    public string? Token { get; set; }

    public LoginResponseDto(string token)
    {
        Token = token;
    }
}
