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
    public class SP_AddUpdSRequisitionDetailsController : ControllerBase
    {
        private readonly ISP_AddUpdSRequisitionDetailsRepository repository;
        private readonly IMapper mapper;

        public SP_AddUpdSRequisitionDetailsController(ISP_AddUpdSRequisitionDetailsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>getasync([FromBody] List<SP_AddUpdSRequisitionDetails> sP_AddUpdSRequisitionDetails)
        {
            try
            {
                var domain = await repository.getasync(sP_AddUpdSRequisitionDetails);
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
