using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LekuTrans.Data.Enums;

namespace LekuTrans.Data.Models;

[Table("cargo")]
public class Cargo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

    [Column("length_m", TypeName = "numeric(8,2)")]
    public decimal LengthM { get; set; }

    [Column("width_m", TypeName = "numeric(8,2)")]
    public decimal WidthM { get; set; }

    [Column("height_m", TypeName = "numeric(8,2)")]
    public decimal HeightM { get; set; }

    [Column("cargo_type")]
    public CargoType CargoType { get; set; } = CargoType.Обычный;

    [Column("special_requirements")]
    public string SpecialRequirements { get; set; }

    public ICollection<ClientCargo> ClientCargos { get; set; }
}