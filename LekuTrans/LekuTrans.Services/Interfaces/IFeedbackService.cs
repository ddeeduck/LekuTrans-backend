using LekuTrans.Data.Models;

namespace LekuTrans.Services.Interfaces;

public interface IFeedbackService
{
    Task<Feedback> AddFeedbackAsync(long? userId, string type, string name, string email, string message);
    Task<IEnumerable<Feedback>> GetAllAsync();
}