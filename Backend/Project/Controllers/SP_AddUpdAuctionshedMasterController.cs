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
    public class SP_AddUpdAuctionshedMasterController : ControllerBase
    {
        private readonly ISP_AddUpdAuctionshedMasterRepository repository;
        private readonly IMapper mapper;

        public SP_AddUpdAuctionshedMasterController(ISP_AddUpdAuctionshedMasterRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync(SP_AddUpdAuctionshedMaster input)
        {
            try
            {
                var domain = await repository.getasync(input);
                var dto = mapper.Map<List<ApiResponseMessage2Dto>>(domain);

                return Ok(dto);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
