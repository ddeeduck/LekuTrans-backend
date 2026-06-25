using LekuTrans.Data.Enums;

namespace LekuTrans.Services.Models;

public class UserDto
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public UserRole Role { get; set; }
    public string FullName { get; set; }
}