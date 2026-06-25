using LekuTrans.Data.Enums;
using LekuTrans.Data.Models;
using LekuTrans.Data.Repositories;
using LekuTrans.Services.Interfaces;

namespace LekuTrans.Services.Services;

public class AssignmentService : IAssignmentService
{
    private readonly IRepository<Assignment> _assignmentRepo;
    private readonly IRepository<Order> _orderRepo;
    private readonly IRepository<Vehicle> _vehicleRepo;
    private readonly IRepository<Driver> _driverRepo;

    public AssignmentService(
        IRepository<Assignment> assignmentRepo,
        IRepository<Order> orderRepo,
        IRepository<Vehicle> vehicleRepo,
        IRepository<Driver> driverRepo)
    {
        _assignmentRepo = assignmentRepo ?? throw new ArgumentNullException(nameof(assignmentRepo));
        _orderRepo = orderRepo ?? throw new ArgumentNullException(nameof(orderRepo));
        _vehicleRepo = vehicleRepo ?? throw new ArgumentNullException(nameof(vehicleRepo));
        _driverRepo = driverRepo ?? throw new ArgumentNullException(nameof(driverRepo));
    }

    public async Task<Assignment> AssignVehicleAsync(long orderId, long vehicleId, long driverId)
    {
        var order = await _orderRepo.GetByIdAsync(orderId);
        var vehicle = await _vehicleRepo.GetByIdAsync(vehicleId);
        var driver = await _driverRepo.GetByIdAsync(driverId);

        if (order == null)
            throw new ArgumentNullException(nameof(order), $"Заказ с ID {orderId} не найден.");
        if (vehicle == null)
            throw new ArgumentNullException(nameof(vehicle), $"Машина с ID {vehicleId} не найдена.");
        if (driver == null)
            throw new ArgumentNullException(nameof(driver), $"Водитель с ID {driverId} не найден.");
        if (vehicle.Status != VehicleStatus.Свободен)
            throw new InvalidOperationException("Машина не свободна.");
        if (driver.Status != DriverStatus.Доступен)
            throw new InvalidOperationException("Водитель не доступен.");

        var assignment = new Assignment
        {
            OrderId = orderId,
            VehicleId = vehicleId,
            DriverId = driverId,
            AssignedAt = DateTime.UtcNow
        };

        vehicle.Status = VehicleStatus.ВРейсе;
        driver.Status = DriverStatus.Занят;
        order.Status = OrderStatus.НазначенТранспорт;

        await _assignmentRepo.CreateAsync(assignment);
        _vehicleRepo.Update(vehicle);
        _driverRepo.Update(driver);
        _orderRepo.Update(order);

        await _assignmentRepo.SaveAsync();

        return assignment;
    }

    public async Task CompleteAssignmentAsync(long assignmentId)
    {
        var assignment = await _assignmentRepo.GetByIdAsync(assignmentId);

        if (assignment == null)
            throw new ArgumentNullException(nameof(assignment), $"Назначение с ID {assignmentId} не найдено.");

        assignment.ActualEnd = DateTime.UtcNow;

        var order = await _orderRepo.GetByIdAsync(assignment.OrderId);
        var vehicle = await _vehicleRepo.GetByIdAsync(assignment.VehicleId);
        var driver = await _driverRepo.GetByIdAsync(assignment.DriverId);

        if (order != null) order.Status = OrderStatus.Доставлен;
        if (vehicle != null) vehicle.Status = VehicleStatus.Свободен;
        if (driver != null) driver.Status = DriverStatus.Доступен;

        _assignmentRepo.Update(assignment);
        if (order != null) _orderRepo.Update(order);
        if (vehicle != null) _vehicleRepo.Update(vehicle);
        if (driver != null) _driverRepo.Update(driver);

        await _assignmentRepo.SaveAsync();
    }
}