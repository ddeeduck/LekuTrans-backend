using LekuTrans.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LekuTrans.Data.Models;

[Table("client_profiles")]
public class ClientProfile
{
    [Key]
    [Column("user_id")]
    public long UserId { get; set; }

    [Column("type")]
    public ClientType Type { get; set; }

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