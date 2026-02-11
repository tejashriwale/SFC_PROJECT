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
    public class SP_AddUpdSRequisitionMasterController : ControllerBase
    {
        private readonly ISP_AddUpdSRequisitionMasterRepository repository;
        private readonly IMapper mapper;

        public SP_AddUpdSRequisitionMasterController(ISP_AddUpdSRequisitionMasterRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>getasync(SP_AddUpdSRequisitionMaster sP_AddUpdSRequisitionMaster)
        {
            try
            {
                var domain = await repository.getasync(sP_AddUpdSRequisitionMaster);
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
