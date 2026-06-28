namespace LekuTrans.Services.Models;

public class AddFeedbackDto
{
    public long? UserId { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
}