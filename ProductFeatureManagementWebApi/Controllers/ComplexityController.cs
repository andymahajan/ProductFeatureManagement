using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductFeatureManagementWebApi
{
    using Microsoft.AspNetCore.Mvc;
    using ProductFeatureManagementWebApi.Models;
    using ProductFeatureManagementWebApi.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class ComplexityController : ControllerBase
    {
        private readonly IComplexityService _complexityService;

        public ComplexityController(IComplexityService complexityService)
        {
            _complexityService = complexityService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Complexity>>> GetAllComplexities()
        {
            try
            {
                var complexities = await _complexityService.GetAllComplexitiesAsync();
                return Ok(complexities);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "Internal server error");
            }
        }
    }

}
