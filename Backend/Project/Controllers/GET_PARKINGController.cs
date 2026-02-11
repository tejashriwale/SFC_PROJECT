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
    public class GET_PARKINGController : ControllerBase
    {
        private readonly IGET_PARKINGRepository repositroy;
        private readonly IMapper mapper;

        public GET_PARKINGController(IGET_PARKINGRepository repositroy, IMapper mapper)
        {
            this.repositroy = repositroy;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync(GET_PARKINGDomain input)
        {
            try
            {

                var domain = await repositroy.getkeyword(input);
                var dto = mapper.Map<List<GET_PARKINGDto>>(domain);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                // log if needed
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        [Route("_Search")]

        public async Task<IActionResult> Get(GET_PARKINGDomain input)
        {
            try
            {
                var domain = await repositroy.getkeyword(input);

                var dto = mapper.Map<List<GET_PARKINGDto>>(domain);

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
