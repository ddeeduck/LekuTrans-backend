using LekuTrans.Data.Enums;

namespace LekuTrans.Services.Models;

public class DriverDto
{
    public string FullName { get; set; }
    public string LicenseNumber { get; set; }
    public DateTime LicenseSince { get; set; }
    public string Phone { get; set; }
    public DriverStatus Status { get; set; }
}