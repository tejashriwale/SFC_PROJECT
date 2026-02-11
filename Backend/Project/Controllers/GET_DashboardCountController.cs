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
    public class GET_DashboardCountController : ControllerBase
    {
        private readonly IGET_DashboardCountRepository repository;
        private readonly IMapper mapper;

        public GET_DashboardCountController(IGET_DashboardCountRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync(GET_DashboardCountDomain customer)
        {
            try
            {
                var domain = await repository.getasync(customer);
                var dto = mapper.Map<List<GET_DashboardCountDto>>(domain);

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
