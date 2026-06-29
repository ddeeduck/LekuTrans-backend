using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LekuTrans.Data.Enums;

namespace LekuTrans.Data.Models;

[Table("orders")]
public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public long Id { get; set; }

    [Column("client_id")]
    public long ClientId { get; set; }

    [Column("client_cargo_id")]
    public long ClientCargoId { get; set; }

    [Column("status")]
    public OrderStatus Status { get; set; } = OrderStatus.НоваяЗаявка;

    [Column("payment_status")]
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.НеОплачен;

    [Column("pickup_address")]
    public string PickupAddress { get; set; }

    [Column("delivery_address")]
    public string DeliveryAddress { get; set; }

    [Column("trailer_type")]
    public string TrailerType { get; set; }

    [Column("insurance")]
    public bool Insurance { get; set; }

    [Column("additional_conditions")]
    public string AdditionalConditions { get; set; }

    [Column("notes")]
    public string Notes { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("ClientId")]
    public ClientProfile Client { get; set; }

    [ForeignKey("ClientCargoId")]
    public ClientCargo ClientCargo { get; set; }

    public LoadingInfo LoadingInfo { get; set; }
    public Recipient Recipient { get; set; }
    public ICollection<OrderStatusHistory> StatusHistory { get; set; }
    public ICollection<Assignment> Assignments { get; set; }
    public ICollection<Review> Reviews { get; set; }
}