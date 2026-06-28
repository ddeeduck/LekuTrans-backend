using LekuTrans.Data.Models;

namespace LekuTrans.Services.Interfaces;

public interface IReviewService
{
    Task<Review> AddReviewAsync(long orderId, long clientId, int rating, string? comment);
    Task<IEnumerable<Review>> GetByOrderAsync(long orderId);
}