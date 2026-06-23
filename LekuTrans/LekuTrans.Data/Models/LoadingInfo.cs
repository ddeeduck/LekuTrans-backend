using LekuTrans.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LekuTrans.Data.Models;

[Table("loading_info")]
public class LoadingInfo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public long Id { get; set; }

    [Column("order_id")]
    public long OrderId { get; set; }

    [Column("loading_dt")]
    public DateTime? LoadingDt { get; set; }

    [Column("unloading_dt")]
    public DateTime? UnloadingDt { get; set; }

    [Column("loading_type")]
    public LoadingType LoadingType { get; set; } = LoadingType.Задняя;

    [ForeignKey("OrderId")]
    public Order Order { get; set; }
}