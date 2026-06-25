using LekuTrans.Data.Enums;
using LekuTrans.Data.Models;
using LekuTrans.Data.Repositories;
using LekuTrans.Services.Interfaces;
using LekuTrans.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace LekuTrans.Services.Services;

public class OrderService : IOrderService
{
    private readonly IRepository<Order> _repository;

    public OrderService(IRepository<Order> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Order> CreateOrderAsync(OrderDto dto)
    {
        var order = new Order
        {
            ClientId = dto.ClientId,
            ClientCargoId = dto.ClientCargoId,
            PickupAddress = dto.PickupAddress,
            DeliveryAddress = dto.DeliveryAddress,
            Status = OrderStatus.НоваяЗаявка,
            PaymentStatus = PaymentStatus.НеОплачен,
            TrailerType = dto.TrailerType,
            Insurance = dto.Insurance,
            AdditionalConditions = dto.AdditionalConditions,
            Notes = dto.Notes,
            LoadingInfo = new LoadingInfo
            {
                LoadingDt = dto.LoadingDt,
                UnloadingDt = dto.UnloadingDt,
                LoadingType = dto.LoadingType
            },
            Recipient = new Recipient
            {
                Company = dto.RecipientCompany,
                Phone = dto.RecipientPhone,
                Inn = dto.RecipientInn
            }
        };

        await _repository.CreateAsync(order);
        await _repository.SaveAsync();

        return order;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _repository.GetQueryAsync().ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(long id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Order>> GetByClientAsync(long clientId)
    {
        return await _repository.GetQueryAsync().Where(o => o.ClientId == clientId).ToListAsync();
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