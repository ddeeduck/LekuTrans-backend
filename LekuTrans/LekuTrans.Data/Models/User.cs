using LekuTrans.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LekuTrans.Data.Models;

[Table("users")]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public long Id { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("password_hash")]
    public string PasswordHash { get; set; }

    [Column("role")]
    public UserRole Role { get; set; } = UserRole.Client;

    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    [Column("full_name")]
    public string FullName { get; set; }

    public ClientProfile ClientProfile { get; set; }
    public ICollection<OrderStatusHistory> StatusHistoryChanges { get; set; }
    public ICollection<Feedback> Feedbacks { get; set; }
}