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
    public class SP_AddUpdSBillMasterController : ControllerBase
    {
        private readonly ISP_AddUpdSBillMasterRepository repository;
        private readonly IMapper mapper;

        public SP_AddUpdSBillMasterController(ISP_AddUpdSBillMasterRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>getasync(SP_AddUpdSBillMaster sP_AddUpdSBillMaster)
        {
            try
            {
                var domain = await repository.getasync(sP_AddUpdSBillMaster);
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
