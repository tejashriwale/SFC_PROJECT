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
    public class SP_AddUpdSChallanDetailsController : ControllerBase
    {
        private readonly ISP_AddUpdSChallanDetailsRepository repository;
        private readonly IMapper mapper;

        public SP_AddUpdSChallanDetailsController(ISP_AddUpdSChallanDetailsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>getasync([FromBody] List<SP_AddUpdSChallanDetails> sP_AddUpdSChallanDetails)
        {
            try
            {
                var domain = await repository.getasync(sP_AddUpdSChallanDetails);
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
