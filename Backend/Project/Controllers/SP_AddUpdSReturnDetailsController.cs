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
    public class SP_AddUpdSReturnDetailsController : ControllerBase
    {
        private readonly ISP_AddUpdSReturnDetailsRepository repository;
        private readonly IMapper mapper;

        public SP_AddUpdSReturnDetailsController(ISP_AddUpdSReturnDetailsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>getasync([FromBody] List<SP_AddUpdSReturnDetails> sP_AddUpdSReturnDetails)
        {
            try
            {
                var domain = await repository.getasync(sP_AddUpdSReturnDetails);
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
