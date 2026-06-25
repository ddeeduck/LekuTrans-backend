using LekuTrans.Data.Enums;
using LekuTrans.Data.Models;
using LekuTrans.Services.Models;

namespace LekuTrans.Services.Interfaces;

public interface IDriverService
{
    Task<Driver> AddDriverAsync(DriverDto dto);
    Task<IEnumerable<Driver>> GetAllAsync();
    Task<IEnumerable<Driver>> GetAvailableAsync();
    Task<Driver?> UpdateStatusAsync(long id, DriverStatus newStatus);
    Task DeleteDriverAsync(long id);
}