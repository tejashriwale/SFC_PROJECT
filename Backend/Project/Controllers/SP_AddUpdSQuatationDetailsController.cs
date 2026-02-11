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
    public class SP_AddUpdSQuatationDetailsController : ControllerBase
    {
        private readonly ISP_AddUpdSQuatationDetailsRepository repository;
        private readonly IMapper mapper;

        public SP_AddUpdSQuatationDetailsController(ISP_AddUpdSQuatationDetailsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>getasync([FromBody] List<SP_AddUpdSQuatationDetails> sP_AddUpdSQuatationDetails)
        {

            try
            {
                var domain = await repository.getasync(sP_AddUpdSQuatationDetails);
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
