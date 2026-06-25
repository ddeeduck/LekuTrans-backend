using LekuTrans.Data.Enums;
using LekuTrans.Data.Models;
using LekuTrans.Data.Repositories;
using LekuTrans.Services.Interfaces;
using LekuTrans.Services.Models;

namespace LekuTrans.Services.Services;

public class DriverService : IDriverService
{
    private readonly IRepository<Driver> _repository;

    public DriverService(IRepository<Driver> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Driver> AddDriverAsync(DriverDto dto)
    {
        var driver = new Driver
        {
            FullName = dto.FullName,
            LicenseNumber = dto.LicenseNumber,
            LicenseSince = dto.LicenseSince,
            Phone = dto.Phone,
            Status = dto.Status
        };

        await _repository.CreateAsync(driver);
        await _repository.SaveAsync();

        return driver;
    }

    public async Task<IEnumerable<Driver>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<IEnumerable<Driver>> GetAvailableAsync()
    {
        var drivers = await _repository.GetAllAsync();
        return drivers.Where(d => d.Status == DriverStatus.Доступен).ToList();
    }

    public async Task<Driver?> UpdateStatusAsync(long id, DriverStatus newStatus)
    {
        var driver = await _repository.GetByIdAsync(id);

        if (driver == null)
            throw new ArgumentNullException(nameof(driver), $"Водитель с ID {id} не найден.");

        driver.Status = newStatus;
        _repository.Update(driver);
        await _repository.SaveAsync();

        return driver;
    }

    public async Task DeleteDriverAsync(long id)
    {
        var driver = await _repository.GetByIdAsync(id);

        if (driver == null)
            throw new ArgumentNullException(nameof(driver), $"Водитель с ID {id} не найден.");

        _repository.Delete(id);
        await _repository.SaveAsync();
    }
}