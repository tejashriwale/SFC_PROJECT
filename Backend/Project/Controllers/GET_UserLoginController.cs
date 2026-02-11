using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Model.Domain;
using Project.Model.DTO;
using Project.Repository;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GET_UserLoginController : ControllerBase
    {
        private readonly IGET_UserProfileSearchRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<GET_UserLoginController> logger;

        public GET_UserLoginController(IGET_UserProfileSearchRepository repository, IMapper mapper, ILogger<GET_UserLoginController> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }
        [HttpPost]

        public async Task<IActionResult>getall(GET_UserProfileSearch input)
        {
            try
            {
                // Log request details
                logger.LogInformation("=== Login Request Started ===");
                logger.LogInformation($"Email: {input?.EMAIL ?? "NULL"}");
                logger.LogInformation($"Password: {(string.IsNullOrEmpty(input?.PASSWORD) ? "NULL/EMPTY" : "PROVIDED")}");
                logger.LogInformation($"User-Agent: {Request.Headers["User-Agent"]}");
                logger.LogInformation($"Origin: {Request.Headers["Origin"]}");
                logger.LogInformation($"Content-Type: {Request.ContentType}");

                if (input == null || string.IsNullOrEmpty(input.EMAIL) || string.IsNullOrEmpty(input.PASSWORD))
                {
                    logger.LogWarning("Invalid input - Email or Password is null/empty");
                    return Ok(new List<GET_UserProfileSearchDto>()); // Return empty array for invalid input
                }

                var domain = await repository.GetAll(input);
                logger.LogInformation($"Repository returned {domain?.Count ?? 0} results");

                var dto = mapper.Map<List<GET_UserProfileSearchDto>>(domain);
                logger.LogInformation($"Mapped to {dto?.Count ?? 0} DTOs");
                logger.LogInformation("=== Login Request Completed ===");

                return Ok(dto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in GET_UserLogin: {Message}", ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
