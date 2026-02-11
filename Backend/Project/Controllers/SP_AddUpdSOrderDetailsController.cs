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
    public class SP_AddUpdSOrderDetailsController : ControllerBase
    {
        private readonly ISP_AddUpdSOrderDetailsRepository repository;
        private readonly IMapper mapper;

        public SP_AddUpdSOrderDetailsController(ISP_AddUpdSOrderDetailsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;

        }
        [HttpPost]

        public async Task<IActionResult>getasync([FromBody] List<SP_AddUpdSOrderDetails> p_AddUpdSOrderDetails)
        {
            try
            {
                var domain = await repository.getasync(p_AddUpdSOrderDetails);
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
