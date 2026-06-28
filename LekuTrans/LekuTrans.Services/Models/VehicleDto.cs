using LekuTrans.Data.Enums;

namespace LekuTrans.Services.Models;

public class VehicleDto
{
    public string LicensePlate { get; set; }
    public string Model { get; set; }
    public decimal CapacityKg { get; set; }
    public decimal CapacityM3 { get; set; }
    public VehicleStatus Status { get; set; }
}