using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceAPI.DTOs;

namespace EcommerceAPI.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDTO>> GetAllReviewsAsync();
        Task<ReviewDTO> GetReviewByIdAsync(int id);
        Task AddReviewAsync(ReviewDTO reviewDTO);
        Task UpdateReviewAsync(int id, ReviewDTO reviewDTO);
        Task DeleteReviewAsync(int id);
    }
}