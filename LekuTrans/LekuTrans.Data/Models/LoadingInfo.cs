using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LekuTrans.Data.Models;

[Table("loading_info")]
public class LoadingInfo
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("order_id")]
    public long OrderId { get; set; }

    [Column("loading_date")]
    public DateTime? LoadingDate { get; set; }

    [Column("loading_time")]
    public TimeSpan? LoadingTime { get; set; }

    [Column("unloading_date")]
    public DateTime? UnloadingDate { get; set; }

    [Column("unloading_time")]
    public TimeSpan? UnloadingTime { get; set; }

    [Column("loading_type")]
    public string LoadingType { get; set; } = "задн€€";

    [ForeignKey("OrderId")]
    public Order Order { get; set; }
}