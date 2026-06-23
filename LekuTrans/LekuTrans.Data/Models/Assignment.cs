using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LekuTrans.Data.Models;

[Table("assignments")]
public class Assignment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public long Id { get; set; }

    [Column("order_id")]
    public long OrderId { get; set; }

    [Column("vehicle_id")]
    public long VehicleId { get; set; }

    [Column("driver_id")]
    public long DriverId { get; set; }

    [Column("assigned_at")]
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

    [Column("planned_start")]
    public DateTime? PlannedStart { get; set; }

    [Column("planned_end")]
    public DateTime? PlannedEnd { get; set; }

    [Column("actual_start")]
    public DateTime? ActualStart { get; set; }

    [Column("actual_end")]
    public DateTime? ActualEnd { get; set; }

    [ForeignKey("OrderId")]
    public Order Order { get; set; }

    [ForeignKey("VehicleId")]
    public Vehicle Vehicle { get; set; }

    [ForeignKey("DriverId")]
    public Driver Driver { get; set; }
}