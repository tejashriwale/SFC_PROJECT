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
    public class SP_AddUpdPOrderDetailsController : ControllerBase
    {
        private readonly ISP_AddUpdPOrderDetailsRepository repository;
        private readonly IMapper mapper;

        public SP_AddUpdPOrderDetailsController(ISP_AddUpdPOrderDetailsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> AddMultipleAsync([FromBody] List<SP_AddUpdPOrderDetails> pOrderDetails)
        {

            try
            {
                var domain = await repository.getasync(pOrderDetails);
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
