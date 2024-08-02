using System.ComponentModel.DataAnnotations;

public class LoginRequest
{
    [Key]
    public string Email { get; set; }
    public string Password { get; set; }
}
