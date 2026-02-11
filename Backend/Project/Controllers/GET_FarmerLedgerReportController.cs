using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Model.Domain;
using Project.Model.DTO;
using Project.Repository;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GET_FarmerLedgerReportController : ControllerBase
    {
        private readonly IGET_FarmerLedgerReportRepository repository;
        private readonly IMapper mapper;

        public GET_FarmerLedgerReportController(IGET_FarmerLedgerReportRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> getasync(GET_FarmerLedgerReportDomain input)
        {
            try
            {
                var domain = await repository.getasync(input);
                var dto = mapper.Map<List<GET_FarmerLedgerReportDto>>(domain);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
