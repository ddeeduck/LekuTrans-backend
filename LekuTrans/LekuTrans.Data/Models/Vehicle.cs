using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LekuTrans.Data.Enums;

namespace LekuTrans.Data.Models;

[Table("vehicles")]
public class Vehicle
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("license_plate")]
    public string LicensePlate { get; set; }

    [Column("model")]
    public string Model { get; set; }

    [Column("capacity_kg", TypeName = "numeric(10,2)")]
    public decimal CapacityKg { get; set; }

    [Column("capacity_m3", TypeName = "numeric(10,2)")]
    public decimal CapacityM3 { get; set; }

    [Column("status")]
    public VehicleStatus Status { get; set; } = VehicleStatus.Свободен;

    public ICollection<Assignment> Assignments { get; set; }
}