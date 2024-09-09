namespace ProductFeatureManagementWebApi
{
    using FluentAssertions;
    using Microsoft.Extensions.Logging;
    using Moq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;
    using ProductFeatureManagementWebApi.Models;

    public class FeaturesServiceTests
    {
        private readonly Mock<IFeaturesRepository> _mockRepository;
        private readonly Mock<ILogger<FeaturesService>> _mockLogger;
        private readonly FeaturesService _service;

        public FeaturesServiceTests()
        {
            _mockRepository = new Mock<IFeaturesRepository>();
            _mockLogger = new Mock<ILogger<FeaturesService>>();
            _service = new FeaturesService(_mockRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllFeaturesAsync_ShouldReturnFeatures()
        {
            // Arrange
            var features = new List<FeatureDto>
            {
                new FeatureDto { FeaturesId = 1, Title = "Feature 1" },
                new FeatureDto { FeaturesId = 2, Title = "Feature 2" }
            };
            _mockRepository.Setup(r => r.GetAllFeaturesAsync()).ReturnsAsync(features);

            // Act
            var result = await _service.GetAllFeaturesAsync();

            // Assert
            result.Should().BeEquivalentTo(features);
        }

        [Fact]
        public async Task GetFeatureByIdAsync_ShouldReturnFeature()
        {
            // Arrange
            var feature = new Feature { FeaturesId = 1, Title = "Feature 1" };
            _mockRepository.Setup(r => r.GetFeatureByIdAsync(1)).ReturnsAsync(feature);

            // Act
            var result = await _service.GetFeatureByIdAsync(1);

            // Assert
            result.Should().Be(feature);
        }

        [Fact]
        public async Task AddFeatureAsync_ShouldReturnAddedFeature()
        {
            // Arrange
            var feature = new Feature { FeaturesId = 1, Title = "Feature 1" };
            _mockRepository.Setup(r => r.AddFeatureAsync(feature)).ReturnsAsync(feature);

            // Act
            var result = await _service.AddFeatureAsync(feature);

            // Assert
            result.Should().Be(feature);
        }

        [Fact]
        public async Task UpdateFeatureAsync_ShouldReturnUpdatedFeature()
        {
            // Arrange
            var feature = new Feature { FeaturesId = 1, Title = "Feature 1" };
            _mockRepository.Setup(r => r.UpdateFeatureAsync(feature)).ReturnsAsync(feature);

            // Act
            var result = await _service.UpdateFeatureAsync(feature);

            // Assert
            result.Should().Be(feature);
        }

        [Fact]
        public async Task DeleteFeatureAsync_ShouldReturnTrue()
        {
            // Arrange
            _mockRepository.Setup(r => r.DeleteFeatureAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _service.DeleteFeatureAsync(1);

            // Assert
            result.Should().BeTrue();
        }
    }

}
