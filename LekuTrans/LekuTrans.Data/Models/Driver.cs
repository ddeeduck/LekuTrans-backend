using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LekuTrans.Data.Enums;

namespace LekuTrans.Data.Models;

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
    public DriverStatus Status { get; set; } = DriverStatus.Доступен;

    public ICollection<Assignment> Assignments { get; set; }
}