using LekuTrans.Data.Enums;
using LekuTrans.Data.Models;
using LekuTrans.Data.Repositories;
using LekuTrans.Services.Interfaces;

namespace LekuTrans.Services.Services;

public class OrderService : IOrderService
{
    private readonly IRepository<Order> _repository;

    public OrderService(IRepository<Order> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Order> CreateOrderAsync(
        long clientId, long clientCargoId, string pickupAddress, string deliveryAddress,
        DateTime? loadingDt, DateTime? unloadingDt, LoadingType loadingType,
        string recipientCompany, string? recipientPhone, string? recipientInn,
        string? trailerType, bool insurance, string? additionalConditions, string? notes)
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

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Order?> GetByIdAsync(long id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Order>> GetByClientAsync(long clientId)
    {
        var orders = await _repository.GetAllAsync();
        return orders.Where(o => o.ClientId == clientId).ToList();
    }

    public async Task<Order?> UpdateStatusAsync(long id, OrderStatus newStatus)
    {
        var order = await _repository.GetByIdAsync(id);

        if (order == null)
            throw new ArgumentNullException(nameof(order), $"Заказ с ID {id} не найден.");

        order.Status = newStatus;
        _repository.Update(order);
        await _repository.SaveAsync();

        return order;
    }
}