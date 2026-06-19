using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LekuTrans_backend.Models;

[Table("cargo")]
public class Cargo
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; } = 1;

    [Column("weight_kg", TypeName = "numeric(10,2)")]
    public decimal WeightKg { get; set; }

    [Column("volume_m3", TypeName = "numeric(10,2)")]
    public decimal VolumeM3 { get; set; }

    [Column("length_cm", TypeName = "numeric(8,2)")]
    public decimal LengthCm { get; set; }

    [Column("width_cm", TypeName = "numeric(8,2)")]
    public decimal WidthCm { get; set; }

    [Column("height_cm", TypeName = "numeric(8,2)")]
    public decimal HeightCm { get; set; }

    [Column("cargo_type")]
    public string CargoType { get; set; } = "обычный";

    [Column("special_requirements")]
    public string SpecialRequirements { get; set; }

    public ICollection<ClientCargo> ClientCargos { get; set; }
}