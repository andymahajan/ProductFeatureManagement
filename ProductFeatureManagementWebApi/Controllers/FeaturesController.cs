using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductFeatureManagementWebApi
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProductFeatureManagementWebApi.Models;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IFeaturesService _featuresService;
        private readonly ILogger<FeaturesController> _logger;

        public FeaturesController(IFeaturesService featuresService, ILogger<FeaturesController> logger)
        {
            _featuresService = featuresService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFeatures()
        {
            _logger.LogInformation("Fetching all features.");
            try
            {
                var features = await _featuresService.GetAllFeaturesAsync();
                return Ok(features);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all features.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeatureById(long id)
        {
            _logger.LogInformation($"Fetching feature with ID {id}.");
            try
            {
                var feature = await _featuresService.GetFeatureByIdAsync(id);
                if (feature == null)
                {
                    _logger.LogWarning($"Feature with ID {id} not found.");
                    return NotFound();
                }
                return Ok(feature);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching feature with ID {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddFeature([FromBody] Feature feature)
        {
            _logger.LogInformation("Adding a new feature.");
            try
            {
                if (feature == null)
                {
                    _logger.LogWarning("Feature data is null.");
                    return BadRequest("Feature data is null");
                }

                var addedFeature = await _featuresService.AddFeatureAsync(feature);
                return CreatedAtAction(nameof(GetFeatureById), new { id = addedFeature.FeaturesId }, addedFeature);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new feature.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFeature(long id, [FromBody] Feature feature)
        {
            _logger.LogInformation($"Updating feature with ID {id}.");
            try
            {
                if (feature == null)
                {
                    _logger.LogWarning("Feature data is null.");
                    return BadRequest("Feature data is null");
                }

                var updatedFeature = await _featuresService.UpdateFeatureAsync(feature);
                if (updatedFeature == null)
                {
                    _logger.LogWarning($"Feature with ID {id} not found.");
                    return NotFound();
                }

                return Ok(updatedFeature);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating feature with ID {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeature(long id)
        {
            _logger.LogInformation($"Deleting feature with ID {id}.");
            try
            {
                var result = await _featuresService.DeleteFeatureAsync(id);
                if (!result)
                {
                    _logger.LogWarning($"Feature with ID {id} not found.");
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting feature with ID {id}.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
