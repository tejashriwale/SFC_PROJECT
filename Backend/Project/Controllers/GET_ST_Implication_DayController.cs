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
    public class GET_ST_Implication_DayController : ControllerBase
    {
        private readonly IGET_ST_Implication_DayRepository repository;
        private readonly IMapper mapper;

        public GET_ST_Implication_DayController(IGET_ST_Implication_DayRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync(GET_ST_Implication_Day input)
        {
            try
            {
                var domain = await repository.GetAll();
                var dto = mapper.Map<List<GET_ST_Implication_DayDto>>(domain);

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
