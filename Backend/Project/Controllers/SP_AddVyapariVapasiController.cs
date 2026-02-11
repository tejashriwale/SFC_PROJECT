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
    public class SP_AddVyapariVapasiController : ControllerBase
    {
        private readonly ISP_AddVyapariVapasiRepository repository;
        private readonly IMapper mapper;

        public SP_AddVyapariVapasiController(ISP_AddVyapariVapasiRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync(List<SP_AddVyapariVapasi> input)
        {
            try
            {
                var domain = await repository.getasync(input);
                var dto = mapper.Map<List<ApiResponseMessageDto>>(domain);

                return Ok(dto);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
