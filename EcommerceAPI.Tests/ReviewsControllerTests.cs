using Xunit;
using EcommerceAPI.Controllers;
using EcommerceAPI.Services;
using EcommerceAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace EcommerceAPI.Tests
{
    public class ReviewsControllerTests
    {
        private readonly Mock<IReviewService> _mockReviewService;
        private readonly Mock<ILogger<ReviewsController>> _mockLogger;
        private readonly ReviewsController _controller;

        public ReviewsControllerTests()
        {
            _mockReviewService = new Mock<IReviewService>();
            _mockLogger = new Mock<ILogger<ReviewsController>>();
            _controller = new ReviewsController(_mockReviewService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetReviews_ReturnsListOfReviews()
        {
            var reviews = new List<ReviewDTO>
            {
                new ReviewDTO { ReviewId = 1, Comment = "Great product!", Rating = 5 },
                new ReviewDTO { ReviewId = 2, Comment = "Not bad", Rating = 3 }
            };
            _mockReviewService.Setup(service => service.GetAllReviewsAsync()).ReturnsAsync(reviews);

            var result = await _controller.GetReviews();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedReviews = Assert.IsType<List<ReviewDTO>>(okResult.Value);
            Assert.Equal(2, returnedReviews.Count);
        }

        [Fact]
        public async Task GetReview_ReturnsReview_WhenReviewExists()
        {
            var review = new ReviewDTO { ReviewId = 1, Comment = "Great product!", Rating = 5 };
            _mockReviewService.Setup(service => service.GetReviewByIdAsync(1)).ReturnsAsync(review);

            var result = await _controller.GetReview(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedReview = Assert.IsType<ReviewDTO>(okResult.Value);
            Assert.Equal(1, returnedReview.ReviewId);
        }

        [Fact]
        public async Task GetReview_ReturnsNotFound_WhenReviewDoesNotExist()
        {
            _mockReviewService.Setup(service => service.GetReviewByIdAsync(1)).ReturnsAsync((ReviewDTO)null);

            var result = await _controller.GetReview(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}