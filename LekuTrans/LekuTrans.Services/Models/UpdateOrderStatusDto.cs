using LekuTrans.Data.Enums;

namespace LekuTrans.Services.Models;

public class UpdateOrderStatusDto
{
    public OrderStatus Status { get; set; }
}