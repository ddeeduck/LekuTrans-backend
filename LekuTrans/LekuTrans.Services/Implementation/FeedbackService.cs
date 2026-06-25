using LekuTrans.Data.Models;
using LekuTrans.Data.Repositories;
using LekuTrans.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LekuTrans.Services.Services;

public class FeedbackService : IFeedbackService
{
    private readonly IRepository<Feedback> _repository;

    public FeedbackService(IRepository<Feedback> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Feedback> AddFeedbackAsync(long? userId, string type, string name, string email, string message)
    {
        var feedback = new Feedback
        {
            UserId = userId,
            Type = type,
            Name = name,
            Email = email,
            Message = message
        };

        await _repository.CreateAsync(feedback);
        await _repository.SaveAsync();

        return feedback;
    }

    public async Task<IEnumerable<Feedback>> GetAllAsync()
    {
        return await _repository.GetQueryAsync().ToListAsync();
    }
}