using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LekuTrans.Data.Models;

[Table("recipients")]
public class Recipient
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public long Id { get; set; }

    [Column("order_id")]
    public long OrderId { get; set; }

    [Column("company")]
    public string Company { get; set; }

    [Column("phone")]
    public string Phone { get; set; }

    [Column("inn")]
    public string Inn { get; set; }

    [ForeignKey("OrderId")]
    public Order Order { get; set; }
}