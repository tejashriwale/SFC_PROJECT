using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GET_EMPLOYEEController : ControllerBase
    {
        private readonly IGET_EMPLOYEERepository repository;
        private readonly IMapper mapper;

        public GET_EMPLOYEEController(IGET_EMPLOYEERepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>getasync(GET_EMPLOYEEDomain input)
        {
            try
            {
                var domain = await repository.getasync(input);
                var dto = mapper.Map<List<GET_EMPLOYEEDto>>(domain);

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
