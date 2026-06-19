using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LekuTrans_backend.Models;

[Table("client_profiles")]
public class ClientProfile
{
    [Key]
    [Column("user_id")]
    public long UserId { get; set; }

    [Column("type")]
    public string Type { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("UserId")]
    public User User { get; set; }

    public CompanyClient CompanyClient { get; set; }
    public IndividualClient IndividualClient { get; set; }
    public ICollection<ClientCargo> ClientCargos { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<Review> Reviews { get; set; }
}