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
    public class SP_AddUpdTransportersAppController : ControllerBase
    {
        private readonly ISP_AddUpdTransportersAppRepository repository;
        private readonly IMapper mapper;

        public SP_AddUpdTransportersAppController(ISP_AddUpdTransportersAppRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync(SP_AddUpdTransportersApp input)
        {
            try
            {
                var domain = await repository.getasync(input);
                var dto = mapper.Map<List<SP_AddUpdTransportersApp2Dto>>(domain);

                return Ok(dto);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
