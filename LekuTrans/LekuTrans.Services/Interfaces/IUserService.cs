using LekuTrans.Data.Enums;
using LekuTrans.Data.Models;

namespace LekuTrans.Services.Interfaces;

public interface IUserService
{
    Task<User> RegisterAsync(string email, string passwordHash, UserRole role, string fullName);
    Task<User?> LoginAsync(string email, string passwordHash);
    Task<User?> GetProfileAsync(long id);
}