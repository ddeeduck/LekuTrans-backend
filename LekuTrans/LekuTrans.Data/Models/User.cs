using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LekuTrans.Data.Enums;

namespace LekuTrans.Data.Models;

[Table("users")]
public class User
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("password_hash")]
    public string PasswordHash { get; set; }

    [Column("role")]
    public UserRole Role { get; set; } = UserRole.Client;

    [Column("status")]
    public string Status { get; set; } = "active";

    [Column("full_name")]
    public string FullName { get; set; }

    public ClientProfile ClientProfile { get; set; }
    public ICollection<OrderStatusHistory> StatusHistoryChanges { get; set; }
    public ICollection<Feedback> Feedbacks { get; set; }
}