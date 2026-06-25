using LekuTrans.Data.Enums;
using LekuTrans.Data.Models;
using LekuTrans.Data.Repositories;
using LekuTrans.Services.Interfaces;

namespace LekuTrans.Services.Services;

public class VehicleService : IVehicleService
{
    private readonly IRepository<Vehicle> _repository;

    public VehicleService(IRepository<Vehicle> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Vehicle> AddVehicleAsync(string licensePlate, string model, decimal capacityKg, decimal capacityM3, VehicleStatus vehicleStatus)
    {
        var vehicle = new Vehicle
        {
            LicensePlate = licensePlate,
            Model = model,
            CapacityKg = capacityKg,
            CapacityM3 = capacityM3,
            Status = vehicleStatus
        };

        await _repository.CreateAsync(vehicle);
        await _repository.SaveAsync();

        return vehicle;
    }

    public async Task<IEnumerable<Vehicle>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Vehicle?> GetByIdAsync(long id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Vehicle>> GetAvailableAsync()
    {
        var vehicles = await _repository.GetAllAsync();
        return vehicles.Where(v => v.Status == VehicleStatus.Свободен).ToList();
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