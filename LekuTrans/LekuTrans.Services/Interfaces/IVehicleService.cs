using LekuTrans.Data.Enums;
using LekuTrans.Data.Models;

namespace LekuTrans.Services.Interfaces;

public interface IVehicleService
{
    Task<Vehicle> AddVehicleAsync(string licensePlate, string model, decimal capacityKg, decimal capacityM3, VehicleStatus vehicleStatus);
    Task<IEnumerable<Vehicle>> GetAllAsync();
    Task<Vehicle?> GetByIdAsync(long id);
    Task<IEnumerable<Vehicle>> GetAvailableAsync();
    Task<Vehicle?> UpdateStatusAsync(long id, VehicleStatus newStatus);
    Task DeleteVehicleAsync(long id);
}