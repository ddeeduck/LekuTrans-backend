using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LekuTrans.Data.Enums;
using LekuTrans.Data.Models;
using LekuTrans.Data.Repositories;

namespace LekuTrans.Services
{
    internal class FeedbackService
    {
        private readonly IRepository<Feedback> _repository;

        public FeedbackService(IRepository<Feedback> repository)
        {
            _repository = repository;
        }

        public async Task<Feedback> AddFeedback(long? userId, string type, string name, string email, string message)
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

        public async Task<IEnumerable<Feedback>> GetAllFeedback()
        {
            IEnumerable<Feedback> feedbacks = await _repository.GetAllAsync();

            return feedbacks;
        }
    }
}
