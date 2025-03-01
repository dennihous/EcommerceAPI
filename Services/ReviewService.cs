using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceAPI.Models;
using EcommerceAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Services
{
    public class ReviewService : IReviewService
    {
        private readonly EcommerceContext _context;

        public ReviewService(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReviewDTO>> GetAllReviewsAsync()
        {
            return await _context.Reviews
                .Select(r => new ReviewDTO
                {
                    ReviewId = r.ReviewId,
                    Comment = r.Comment,
                    Rating = r.Rating,
                    ProductId = r.ProductId,
                    CustomerId = r.CustomerId
                })
                .ToListAsync();
        }

        public async Task<ReviewDTO> GetReviewByIdAsync(int id)
        {
            var review = await _context.Reviews
                .Where(r => r.ReviewId == id)
                .Select(r => new ReviewDTO
                {
                    ReviewId = r.ReviewId,
                    Comment = r.Comment,
                    Rating = r.Rating,
                    ProductId = r.ProductId,
                    CustomerId = r.CustomerId
                })
                .FirstOrDefaultAsync();

            if (review == null)
            {
                throw new KeyNotFoundException("Review not found.");
            }

            return review;
        }

        public async Task AddReviewAsync(ReviewDTO reviewDTO)
        {
            var review = new Review
            {
                Comment = reviewDTO.Comment,
                Rating = reviewDTO.Rating,
                ProductId = reviewDTO.ProductId,
                CustomerId = reviewDTO.CustomerId
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReviewAsync(int id, ReviewDTO reviewDTO)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                throw new KeyNotFoundException("Review not found.");
            }

            review.Comment = reviewDTO.Comment;
            review.Rating = reviewDTO.Rating;
            review.ProductId = reviewDTO.ProductId;
            review.CustomerId = reviewDTO.CustomerId;

            _context.Entry(review).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReviewAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                throw new KeyNotFoundException("Review not found.");
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }
    }
}