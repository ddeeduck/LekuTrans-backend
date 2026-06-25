using LekuTrans.Data.Enums;
using LekuTrans.Data.Models;

namespace LekuTrans.Services.Interfaces;

public interface IDriverService
{
    Task<Driver> AddDriverAsync(string fullName, string licenseNumber, DateTime licenseSince, string phone, DriverStatus status);
    Task<IEnumerable<Driver>> GetAllAsync();
    Task<IEnumerable<Driver>> GetAvailableAsync();
    Task<Driver?> UpdateStatusAsync(long id, DriverStatus newStatus);
    Task DeleteDriverAsync(long id);
}