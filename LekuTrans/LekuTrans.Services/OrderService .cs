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
        private readonly IRepository<Order> _repository;

        public OrderService(IRepository<Order> repository)
        {
            _repository = repository;
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

            await _repository.CreateAsync(order);
            await _repository.SaveAsync();

            return order;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            IEnumerable<Order> orders = await _repository.GetAllAsync();

            return orders;
        }

        public async Task<IEnumerable<Order>> GetOrdersByClient(long clientId)
        {
            IEnumerable<Order> orders = await _repository.GetAllAsync();

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
            Order order = await _repository.GetByIdAsync(id);

            if (order == null)
            {
                throw new InvalidOperationException($"Заказ с ID {id} не найден.");
            }

            order.Status = newStatus;

            _repository.Update(order);

            await _repository.SaveAsync();

            return order;
        }
    }
}
