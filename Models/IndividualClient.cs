using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LekuTrans_backend.Models;

[Table("individual_clients")]
public class IndividualClient
{
    [Key]
    [Column("user_id")]
    public long UserId { get; set; }

    [Column("first_name")]
    public string FirstName { get; set; }

    [Column("last_name")]
    public string LastName { get; set; }

    [Column("middle_name")]
    public string MiddleName { get; set; }

    [Column("phone")]
    public string Phone { get; set; }

    [Column("shipping_address")]
    public string ShippingAddress { get; set; }

    [Column("billing_address")]
    public string BillingAddress { get; set; }

    [ForeignKey("UserId")]
    public ClientProfile ClientProfile { get; set; }
}