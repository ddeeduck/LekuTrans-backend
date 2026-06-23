using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LekuTrans.Data.Models;

[Table("reviews")]
public class Review
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public long Id { get; set; }

    [Column("order_id")]
    public long OrderId { get; set; }

    [Column("client_id")]
    public long ClientId { get; set; }

    [Column("rating")]
    public int Rating { get; set; }

    [Column("comment")]
    public string Comment { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("OrderId")]
    public Order Order { get; set; }

    [ForeignKey("ClientId")]
    public ClientProfile Client { get; set; }
}