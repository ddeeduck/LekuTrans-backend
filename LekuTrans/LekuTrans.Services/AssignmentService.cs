using LekuTrans.Data.Enums;
using LekuTrans.Data.Models;
using LekuTrans.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LekuTrans.Services;

public class AssignmentService
{
    private readonly IRepository<Assignment> _assignmentRepository;
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<Vehicle> _vehicleRepository;
    private readonly IRepository<Driver> _driverRepository;

    public AssignmentService(
        IRepository<Assignment> assignmentRepository,
        IRepository<Order> orderRepository,
        IRepository<Vehicle> vehicleRepository,
        IRepository<Driver> driverRepository)
    {
        _assignmentRepository = assignmentRepository;
        _orderRepository = orderRepository;
        _vehicleRepository = vehicleRepository;
        _driverRepository = driverRepository;
    }

    public async Task<Assignment> AssignVehicle(long orderId, long vehicleId, long driverId)
    {
        Order order = await _orderRepository.GetByIdAsync(orderId);
        Vehicle vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);
        Driver driver = await _driverRepository.GetByIdAsync(driverId);

        if (order == null)
        {
            throw new Exception($"Заказ с ID {orderId} не найден.");
        }

        if (vehicle == null)
        {
            throw new Exception($"Машина с ID {vehicleId} не найдена.");
        }

        if (driver == null)
        {
            throw new Exception($"Водитель с ID {driverId} не найден.");
        }

        if (vehicle.Status != VehicleStatus.Свободен)
        {
            throw new Exception("Машина не свободна.");
        }

        if (driver.Status != DriverStatus.Доступен)
        {
            throw new Exception("Водитель не доступен.");
        }

        Assignment assignment = new Assignment
        {
            OrderId = orderId,
            VehicleId = vehicleId,
            DriverId = driverId,
            AssignedAt = DateTime.UtcNow
        };

        vehicle.Status = VehicleStatus.ВРейсе;
        driver.Status = DriverStatus.Занят;
        order.Status = OrderStatus.НазначенТранспорт;

        await _assignmentRepository.CreateAsync(assignment);
        _vehicleRepository.Update(vehicle);
        _driverRepository.Update(driver);
        _orderRepository.Update(order);

        await _assignmentRepository.SaveAsync();

        return assignment;
    }

    public async Task CompleteAssignment(long assignmentId)
    {
        Assignment assignment = await _assignmentRepository.GetByIdAsync(assignmentId);

        if (assignment == null)
        {
            throw new InvalidOperationException($"Назначение с ID {assignmentId} не найдено.");
        }

        assignment.ActualEnd = DateTime.UtcNow;

        Order order = await _orderRepository.GetByIdAsync(assignment.OrderId);
        Vehicle vehicle = await _vehicleRepository.GetByIdAsync(assignment.VehicleId);
        Driver driver = await _driverRepository.GetByIdAsync(assignment.DriverId);

        if (order != null) order.Status = OrderStatus.Доставлен;
        if (vehicle != null) vehicle.Status = VehicleStatus.Свободен;
        if (driver != null) driver.Status = DriverStatus.Доступен;

        _assignmentRepository.Update(assignment);
        if (order != null) _orderRepository.Update(order);
        if (vehicle != null) _vehicleRepository.Update(vehicle);
        if (driver != null) _driverRepository.Update(driver);

        await _assignmentRepository.SaveAsync();
    }
}