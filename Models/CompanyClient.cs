using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LekuTrans_backend.Models;

[Table("company_clients")]
public class CompanyClient
{
    [Key]
    [Column("user_id")]
    public long UserId { get; set; }

    [Column("company_name")]
    public string CompanyName { get; set; }

    [Column("tax_id")]
    public string TaxId { get; set; }

    [Column("registration_number")]
    public string RegistrationNumber { get; set; }

    [Column("legal_address")]
    public string LegalAddress { get; set; }

    [Column("shipping_address")]
    public string ShippingAddress { get; set; }

    [Column("billing_address")]
    public string BillingAddress { get; set; }

    [Column("contact_person_name")]
    public string ContactPersonName { get; set; }

    [Column("contact_person_phone")]
    public string ContactPersonPhone { get; set; }

    [ForeignKey("UserId")]
    public ClientProfile ClientProfile { get; set; }
}