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
    public class GET_StandingI_RateA_ShopAlotController : ControllerBase
    {
        private readonly IGET_StandingI_RateA_ShopAlotRepository repository;
        private readonly IMapper mapper;

        public GET_StandingI_RateA_ShopAlotController(IGET_StandingI_RateA_ShopAlotRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> Get(GET_StandingI_RateA_ShopAlotDomain input)
        {
            try
            {
                var domain = await repository.getasync(input);
                var dto = mapper.Map<List<GET_StandingI_RateA_ShopAlotDto>>(domain);
                return Ok(domain);
            }
            catch (Exception ex)
            {
                // log if needed
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        [Route("_Search")]

        public async Task<IActionResult> getasync([FromBody] GET_StandingI_RateA_ShopAlotDomain input)
        {
            try
            {
                var domain = await repository.getkeyword(input);

                var dto = mapper.Map<List<GET_StandingI_RateA_ShopAlotDto>>(domain);

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
