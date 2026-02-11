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
    public class GET_NotifyFarmerOnTokenGenerationController : ControllerBase
    {
        private readonly IGET_NotifyFarmerOnTokenGenerationRepository repository;
        private readonly IMapper mapper;

        public GET_NotifyFarmerOnTokenGenerationController(IGET_NotifyFarmerOnTokenGenerationRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync(GET_NotifyFarmerOnTokenGenerationDomain input)
        {
            try
            {
                var domain = await repository.getasync(input);
                var dto = mapper.Map<List<GET_NotifyFarmerOnTokenGenerationDto>>(domain);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                // log if needed
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
