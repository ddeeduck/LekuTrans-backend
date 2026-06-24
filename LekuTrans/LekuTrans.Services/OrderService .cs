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
    public class OrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Vehicle> _vehicleRepository;
        private readonly IRepository<Driver> _driverRepository;
        private readonly IRepository<Assignment> _assignmentRepository;

        public OrderService(
            IRepository<Order> orderRepository,
            IRepository<Vehicle> vehicleRepository,
            IRepository<Driver> driverRepository,
            IRepository<Assignment> assignmentRepository)
        {
            _orderRepository = orderRepository;
            _vehicleRepository = vehicleRepository;
            _driverRepository = driverRepository;
            _assignmentRepository = assignmentRepository;
        }

        public async Task<Order> CreateOrder(
            long clientId,
            long clientCargoId,
            string pickupAddress,
            string deliveryAddress,
            DateTime? loadingDt,
            DateTime? unloadingDt,
            LoadingType loadingType,
            string recipientCompany,
            string? recipientPhone,
            string? recipientInn,
            string? trailerType,
            bool insurance,
            string? additionalConditions,
            string? notes)
        {
            var order = new Order
            {
                ClientId = clientId,
                ClientCargoId = clientCargoId,
                PickupAddress = pickupAddress,
                DeliveryAddress = deliveryAddress,
                Status = OrderStatus.НоваяЗаявка,
                PaymentStatus = PaymentStatus.НеОплачен,
                TrailerType = trailerType,
                Insurance = insurance,
                AdditionalConditions = additionalConditions,
                Notes = notes,
                LoadingInfo = new LoadingInfo
                {
                    LoadingDt = loadingDt,
                    UnloadingDt = unloadingDt,
                    LoadingType = loadingType
                },
                Recipient = new Recipient
                {
                    Company = recipientCompany,
                    Phone = recipientPhone,
                    Inn = recipientInn
                }
            };

            await _orderRepository.CreateAsync(order);
            await _orderRepository.SaveAsync();

            return order;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            IEnumerable<Order> orders = await _orderRepository.GetAllAsync();

            return orders;
        }

        public async Task<IEnumerable<Order>> GetOrdersByClient(long clientId)
        {
            IEnumerable<Order> orders = await _orderRepository.GetAllAsync();

            List<Order> resultList = new List<Order>();

            foreach (Order order in orders)
            {
                if (order.ClientId == clientId)
                {
                    resultList.Add(order);
                }
            }

            return resultList;

        }

        public async Task<Order> UpdateStatusOrder(long id, OrderStatus newStatus)
        {
            Order order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
            {
                throw new InvalidOperationException($"Заказ с ID {id} не найден.");
            }

            order.Status = newStatus;

            _orderRepository.Update(order);

            await _orderRepository.SaveAsync();

            return order;
        }

        public async Task AssignVehicle(long orderId, long vehicleId, long driverId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);
            var driver = await _driverRepository.GetByIdAsync(driverId);

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

            await _assignmentRepository.CreateAsync(assignment);
            _vehicleRepository.Update(vehicle);
            _driverRepository.Update(driver);
            _orderRepository.Update(order);

            await _orderRepository.SaveAsync();
        }
    }
}
