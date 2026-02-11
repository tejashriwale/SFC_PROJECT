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
    public class GET_TopVyapariOnCratesController : ControllerBase
    {
        private readonly IGET_TopVyapariOnCratesRepository repository;
        private readonly IMapper mapper;

        public GET_TopVyapariOnCratesController(IGET_TopVyapariOnCratesRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync(GET_TopVyapariOnCratesDomain input)
        {
            try
            {
                var domain = await repository.getasync(input);
                var dto = mapper.Map<List<GET_TopVyapariOnCratesDto>>(domain);

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

        public async Task<IActionResult> Get(GET_TopVyapariOnCratesDomain input)
        {
            try
            {
                var domain = await repository.getkeyword(input);

                var dto = mapper.Map<List<GET_TopVyapariOnCratesDto>>(domain);

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
