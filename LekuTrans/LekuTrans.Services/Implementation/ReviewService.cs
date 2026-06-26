using LekuTrans.Data.Models;
using LekuTrans.Data.Repositories;
using LekuTrans.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LekuTrans.Services.Implementation;

public class ReviewService : IReviewService
{
    private readonly IRepository<Review> _repository;

    public ReviewService(IRepository<Review> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Review> AddReviewAsync(long orderId, long clientId, int rating, string? comment)
    {
        var review = new Review
        {
            OrderId = orderId,
            ClientId = clientId,
            Rating = rating,
            Comment = comment
        };

        await _repository.CreateAsync(review);
        await _repository.SaveAsync();

        return review;
    }

    public async Task<IEnumerable<Review>> GetByOrderAsync(long orderId)
    {
        return await _repository.GetQueryAsync().Where(r => r.OrderId == orderId).ToListAsync();
    }
}