using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductFeatureManagementWebApi.Models;
using Xunit;

namespace ProductFeatureManagementWebApi
{
    public class FeaturesControllerTests
    {
        private readonly FeaturesController _controller;
        private readonly Mock<IFeaturesService> _mockService;
        private readonly Mock<ILogger<FeaturesController>> _mockLogger;

        public FeaturesControllerTests()
        {
            _mockService = new Mock<IFeaturesService>();
            _mockLogger = new Mock<ILogger<FeaturesController>>();
            _controller = new FeaturesController(_mockService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllFeatures_ShouldReturnOkResultWithFeatures()
        {
            // Arrange
            var features = new List<FeatureDto>
            {
                new FeatureDto { FeaturesId = 1, Title = "Feature 1" },
                new FeatureDto { FeaturesId = 2, Title = "Feature 2" }
            };
            _mockService.Setup(s => s.GetAllFeaturesAsync()).ReturnsAsync(features);

            // Act
            var result = await _controller.GetAllFeatures() as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(features);
        }

        [Fact]
        public async Task GetFeatureById_ShouldReturnOkResultWithFeature()
        {
            // Arrange
            var feature = new Feature { FeaturesId = 1, Title = "Feature 1" };
            _mockService.Setup(s => s.GetFeatureByIdAsync(1)).ReturnsAsync(feature);

            // Act
            var result = await _controller.GetFeatureById(1) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().Be(feature);
        }

        [Fact]
        public async Task AddFeature_ShouldReturnCreatedAtActionResult()
        {
            // Arrange
            var feature = new Feature { FeaturesId = 1, Title = "Feature 1" };
            _mockService.Setup(s => s.AddFeatureAsync(feature)).ReturnsAsync(feature);

            // Act
            var result = await _controller.AddFeature(feature) as CreatedAtActionResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(201);
            result.Value.Should().Be(feature);
        }

        [Fact]
        public async Task UpdateFeature_ShouldReturnOkResult()
        {
            // Arrange
            var feature = new Feature { FeaturesId = 1, Title = "Feature 1" };
            _mockService.Setup(s => s.UpdateFeatureAsync(feature)).ReturnsAsync(feature);

            // Act
            var result = await _controller.UpdateFeature(1, feature) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().Be(feature);
        }

        [Fact]
        public async Task DeleteFeature_ShouldReturnNoContentResult()
        {
            // Arrange
            _mockService.Setup(s => s.DeleteFeatureAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteFeature(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
