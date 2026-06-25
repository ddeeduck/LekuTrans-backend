using LekuTrans.Data.Enums;
using LekuTrans.Data.Models;
using LekuTrans.Services.Models;

namespace LekuTrans.Services.Interfaces;

public interface IVehicleService
{
    Task<Vehicle> AddVehicleAsync(VehicleDto dto);
    Task<IEnumerable<Vehicle>> GetAllAsync();
    Task<Vehicle?> GetByIdAsync(long id);
    Task<IEnumerable<Vehicle>> GetAvailableAsync();
    Task<Vehicle?> UpdateStatusAsync(long id, VehicleStatus newStatus);
    Task DeleteVehicleAsync(long id);
}