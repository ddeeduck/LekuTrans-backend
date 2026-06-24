using LekuTrans.Data.Enums;
using LekuTrans.Data.Models;
using LekuTrans.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LekuTrans.Services
{
    public class ReviewService
    {
        private readonly IRepository<Review> _repository;

        public ReviewService(IRepository<Review> repository)
        {
            _repository = repository;
        }

        public async Task<Review> AddReview(long orderId, long clientId, int rating, string? comment)
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

        public async Task<IEnumerable<Review>> GetReviewsByOrder(long orderId)
        {
            IEnumerable<Review> reviews = await _repository.GetAllAsync();

            List<Review> resultList = new List<Review>();

            foreach (Review review in reviews) 
            {
                if (review.OrderId == orderId)
                {
                    resultList.Add(review);
                }
            }

            return resultList;
        }
    }
}
