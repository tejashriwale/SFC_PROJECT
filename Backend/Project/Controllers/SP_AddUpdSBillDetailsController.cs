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
    public class SP_AddUpdSBillDetailsController : ControllerBase
    {
        private readonly ISP_AddUpdSBillDetailsRepository repository;
        private readonly IMapper mapper;

        public SP_AddUpdSBillDetailsController(ISP_AddUpdSBillDetailsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>getasync([FromBody]List<SP_AddUpdSBillDetails>  sP_AddUpdSBillDetails)
        {
            try
            {
                var domain = await repository.getasync(sP_AddUpdSBillDetails);
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
