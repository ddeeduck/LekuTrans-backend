namespace LekuTrans.Services.Models;

public class AddReviewDto
{
    public long OrderId { get; set; }
    public long ClientId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
}