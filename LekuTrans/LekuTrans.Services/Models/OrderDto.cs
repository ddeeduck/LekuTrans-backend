using LekuTrans.Data.Enums;

namespace LekuTrans.Services.Models;

public class OrderDto
{
    public long ClientId { get; set; }
    public long ClientCargoId { get; set; }
    public string PickupAddress { get; set; }
    public string DeliveryAddress { get; set; }
    public DateTime? LoadingDt { get; set; }
    public DateTime? UnloadingDt { get; set; }
    public LoadingType LoadingType { get; set; }
    public string RecipientCompany { get; set; }
    public string? RecipientPhone { get; set; }
    public string? RecipientInn { get; set; }
    public string? TrailerType { get; set; }
    public bool Insurance { get; set; }
    public string? AdditionalConditions { get; set; }
    public string? Notes { get; set; }
}