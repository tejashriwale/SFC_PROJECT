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
    public class GET_AnalyzeDataController : ControllerBase
    {
        private readonly IGET_AnalyzeDataRepository repository;
        private readonly IMapper mapper;

        public GET_AnalyzeDataController(IGET_AnalyzeDataRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>getasync(GET_AnalyzeDataDomain gET_Analyze)
        {
            try
            {
                var domain = await repository.getasync(gET_Analyze);
                var dto = mapper.Map<List<GET_AnalyzeDataDto>>(domain);

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
