using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LekuTrans_backend.Models;

[Table("client_cargo")]
public class ClientCargo
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("client_id")]
    public long ClientId { get; set; }

    [Column("cargo_id")]
    public long CargoId { get; set; }

    [ForeignKey("ClientId")]
    public ClientProfile Client { get; set; }

    [ForeignKey("CargoId")]
    public Cargo Cargo { get; set; }

    public ICollection<Order> Orders { get; set; }
}