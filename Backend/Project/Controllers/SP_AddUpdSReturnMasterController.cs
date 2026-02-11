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
    public class SP_AddUpdSReturnMasterController : ControllerBase
    {
        private readonly ISP_AddUpdSReturnMasterRepository repository;
        private readonly IMapper mapper;

        public SP_AddUpdSReturnMasterController(ISP_AddUpdSReturnMasterRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>getasync(SP_AddUpdSReturnMaster sP_AddUpdSReturnMaster)
        {
            try
            {
                var domain = await repository.getasync(sP_AddUpdSReturnMaster);
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
