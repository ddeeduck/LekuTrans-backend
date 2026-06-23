using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LekuTrans.Data;
using LekuTrans.Data.Models;
using LekuTrans.Data.Enums;
using LekuTrans.Data.Repositories;

namespace LekuTrans.Services
{
    public class UserService
    {
        private readonly IRepository<User> _repository;

        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<User> Register(string email, string passwordHash, UserRole role, string fullName)
        {
            User user = new User()
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

        public async Task<User> Login(string email, string passwordHash)
        {
            IEnumerable<User> users = await _repository.GetAllAsync();
            User user = users.FirstOrDefault(u => u.Email == email); 

            if (user == null || user.PasswordHash != passwordHash) 
            {
                throw new Exception("Неверный email или пароль.");
            }

            return user;
        }

        public async Task<User> GetProfile(long id)
        {
            User user = await _repository.GetByIdAsync(id);

            if (user == null)
            {
                throw new Exception($"Пользователь с ID {id} не существует.");
            }

            return user;
        }
    }
}
