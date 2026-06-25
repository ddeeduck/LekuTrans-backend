using LekuTrans.Data.Enums;
using LekuTrans.Data.Models;
using LekuTrans.Data.Repositories;
using LekuTrans.Services.Interfaces;
using LekuTrans.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace LekuTrans.Services.Services;

public class VehicleService : IVehicleService
{
    private readonly IRepository<Vehicle> _repository;

    public VehicleService(IRepository<Vehicle> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Vehicle> AddVehicleAsync(VehicleDto dto)
    {
        var vehicle = new Vehicle
        {
            LicensePlate = dto.LicensePlate,
            Model = dto.Model,
            CapacityKg = dto.CapacityKg,
            CapacityM3 = dto.CapacityM3,
            Status = dto.Status
        };

        await _repository.CreateAsync(vehicle);
        await _repository.SaveAsync();

        return vehicle;
    }

    public async Task<IEnumerable<Vehicle>> GetAllAsync()
    {
        return await _repository.GetQuery().ToListAsync();
    }

    public async Task<Vehicle?> GetByIdAsync(long id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Vehicle>> GetAvailableAsync()
    {
        return await _repository.GetQuery().Where(v => v.Status == VehicleStatus.Свободен).ToListAsync();
    }

    public async Task<Vehicle?> UpdateStatusAsync(long id, VehicleStatus newStatus)
    {
        var vehicle = await _repository.GetByIdAsync(id);

        if (vehicle == null)
            throw new ArgumentNullException(nameof(vehicle), $"Машина с ID {id} не найдена.");

        vehicle.Status = newStatus;
        _repository.Update(vehicle);
        await _repository.SaveAsync();

        return vehicle;
    }

    public async Task DeleteVehicleAsync(long id)
    {
        var vehicle = await _repository.GetByIdAsync(id);

        if (vehicle == null)
            throw new ArgumentNullException(nameof(vehicle), $"Машина с ID {id} не найдена.");

        _repository.Delete(id);
        await _repository.SaveAsync();
    }
}