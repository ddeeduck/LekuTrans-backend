using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LekuTrans.Data.Models;
using LekuTrans.Data.Enums;
using LekuTrans.Data.Repositories;

namespace LekuTrans.Services
{
    public class DriverService
    {
        private readonly IRepository<Driver> _repository;

        public DriverService(IRepository<Driver> repository)
        {
            _repository = repository;
        }

        public async Task<Driver> AddDriver(string fullName, string licenseNumber, DateTime licenseSince, string phone, DriverStatus status)
        {
            Driver driver = new Driver()
            {
                FullName = fullName,
                LicenseNumber = licenseNumber,
                LicenseSince = licenseSince,
                Phone = phone,
                Status = status
            };

            await _repository.CreateAsync(driver);
            await _repository.SaveAsync();

            return driver;
        }

        public async Task<IEnumerable<Driver>> GetAllDrivers()
        {
            IEnumerable<Driver> drivers = await _repository.GetAllAsync();

            return drivers;
        }

        public async Task<IEnumerable<Driver>> GetAvailableDrivers()
        {
            IEnumerable<Driver> drivers = await _repository.GetAllAsync();

            List<Driver> resultList = new List<Driver>();

            foreach (Driver driver in drivers)
            {
                if (driver.Status == DriverStatus.Доступен)
                {
                    resultList.Add(driver);
                }
            }

            return resultList;

        }

        public async Task<Driver> UpdateStatusDriver(long id, DriverStatus newStatus)
        {
            Driver driver = await _repository.GetByIdAsync(id);

            if (driver == null)
            {
                throw new InvalidOperationException($"Водитель с ID {id} не найден.");
            }

            driver.Status = newStatus;

            _repository.Update(driver);

            await _repository.SaveAsync();

            return driver;
        }

        public async Task DeleteDriver(long id)
        {
            Driver driver = await _repository.GetByIdAsync(id);

            if (driver == null)
            {
                throw new InvalidOperationException($"Водитель с ID {id} не найден.");
            }

            _repository.Delete(id);

            await _repository.SaveAsync();
        }
    }
}
