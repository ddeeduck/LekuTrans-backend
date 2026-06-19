using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LekuTrans_backend.Models;

[Table("drivers")]
public class Driver
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("full_name")]
    public string FullName { get; set; }

    [Column("license_number")]
    public string LicenseNumber { get; set; }

    [Column("license_since")]
    public DateTime? LicenseSince { get; set; }

    [Column("phone")]
    public string Phone { get; set; }

    [Column("status")]
    public string Status { get; set; } = "доступен";

    public ICollection<Assignment> Assignments { get; set; }
}