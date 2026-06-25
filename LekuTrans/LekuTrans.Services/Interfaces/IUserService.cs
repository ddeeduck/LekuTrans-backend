using LekuTrans.Data.Enums;
using LekuTrans.Data.Models;
using LekuTrans.Services.Models;

namespace LekuTrans.Services.Interfaces;

public interface IUserService
{
    Task<User> RegisterAsync(UserDto dto);
    Task<User?> LoginAsync(string email, string passwordHash);
    Task<User?> GetProfileAsync(long id);
}