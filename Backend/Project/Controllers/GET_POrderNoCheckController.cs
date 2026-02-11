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
    public class GET_POrderNoCheckController : ControllerBase
    {
        private readonly IGET_POrderNoCheckRepository repositroy;
        private readonly IMapper mapper;

        public GET_POrderNoCheckController(IGET_POrderNoCheckRepository repositroy, IMapper mapper)
        {
            this.repositroy = repositroy;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync(GET_POrderNoCheckDomain input)
        {
            try
            {

                var domain = await repositroy.getasync(input);
                var dto = mapper.Map<List<GET_POrderNoCheckDto>>(domain);

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
