using LekuTrans.Data.Enums;
using LekuTrans.Data.Models;
using LekuTrans.Data.Repositories;
using LekuTrans.Services.Interfaces;

namespace LekuTrans.Services.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _repository;

    public UserService(IRepository<User> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<User> RegisterAsync(string email, string passwordHash, UserRole role, string fullName)
    {
        var user = new User
        {
            Email = email,
            PasswordHash = passwordHash,
            Role = role,
            FullName = fullName
        };

        await _repository.CreateAsync(user);
        await _repository.SaveAsync();

        return user;
    }

    public async Task<User?> LoginAsync(string email, string passwordHash)
    {
        var users = await _repository.GetAllAsync();
        var user = users.FirstOrDefault(u => u.Email == email);

        if (user == null || user.PasswordHash != passwordHash)
            return null;

        return user;
    }

    public async Task<User?> GetProfileAsync(long id)
    {
        var user = await _repository.GetByIdAsync(id);
        return user;
    }
}