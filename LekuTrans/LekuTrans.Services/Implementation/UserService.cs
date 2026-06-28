using LekuTrans.Data.Enums;
using LekuTrans.Data.Models;
using LekuTrans.Data.Repositories;
using LekuTrans.Services.Interfaces;
using LekuTrans.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace LekuTrans.Services.Implementation;

public class UserService : IUserService
{
    private readonly IRepository<User> _repository;

    public UserService(IRepository<User> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<User> RegisterAsync(UserDto dto)
    {
        var user = new User
        {
            Email = dto.Email,
            PasswordHash = dto.PasswordHash,
            Role = dto.Role,
            FullName = dto.FullName
        };

        await _repository.CreateAsync(user);
        await _repository.SaveAsync();

        return user;
    }

    public async Task<User?> LoginAsync(string email, string passwordHash)
    {
        return await _repository.GetQueryAsync().FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == passwordHash);
    }

    public async Task<User?> GetProfileAsync(long id)
    {
        var user = await _repository.GetByIdAsync(id);
        return user;
    }
}