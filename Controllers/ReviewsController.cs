using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceAPI.Models;
using EcommerceAPI.DTOs;
using EcommerceAPI.Services;
using Microsoft.Extensions.Logging;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly ILogger<ReviewsController> _logger;

        public ReviewsController(IReviewService reviewService, ILogger<ReviewsController> logger)
        {
            _reviewService = reviewService;
            _logger = logger;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetReviews()
        {
            _logger.LogInformation("Fetching all reviews.");
            var reviews = await _reviewService.GetAllReviewsAsync();
            return Ok(reviews);
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDTO>> GetReview(int id)
        {
            _logger.LogInformation($"Fetching review with ID {id}.");
            var review = await _reviewService.GetReviewByIdAsync(id);

            if (review == null)
            {
                _logger.LogWarning($"Review with ID {id} not found.");
                return NotFound();
            }

            return Ok(review);
        }

        // POST: api/Reviews
        [HttpPost]
        public async Task<ActionResult<ReviewDTO>> PostReview(ReviewDTO reviewDTO)
        {
            _logger.LogInformation("Creating a new review.");
            await _reviewService.AddReviewAsync(reviewDTO);
            return CreatedAtAction("GetReview", new { id = reviewDTO.ReviewId }, reviewDTO);
        }

        // PUT: api/Reviews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, ReviewDTO reviewDTO)
        {
            _logger.LogInformation($"Updating review with ID {id}.");
            if (id != reviewDTO.ReviewId)
            {
                _logger.LogWarning("Review ID mismatch.");
                return BadRequest();
            }

            try
            {
                await _reviewService.UpdateReviewAsync(id, reviewDTO);
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning($"Review with ID {id} not found for update.");
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            _logger.LogInformation($"Deleting review with ID {id}.");
            try
            {
                await _reviewService.DeleteReviewAsync(id);
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning($"Review with ID {id} not found for deletion.");
                return NotFound();
            }

            return NoContent();
        }
    }
}