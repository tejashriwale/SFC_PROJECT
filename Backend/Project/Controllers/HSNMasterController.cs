using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Model.Domain;
using Project.Model.Model;
using Project.Repository;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HSNMasterController : ControllerBase
    {
        private readonly IHSNMasterRepository hSNMasterRepository;
        private readonly IMapper mapper;

        public HSNMasterController(IHSNMasterRepository hSNMasterRepository, IMapper mapper)
        {
            this.hSNMasterRepository = hSNMasterRepository;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> HSNMasterAsync([FromBody]HSNMaster hSNMaster)
        {
            try
            {
                var domain = await hSNMasterRepository.HSNMasterAsync(hSNMaster);

                var dto = mapper.Map<List<ApiResponseMessage>>(domain);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                // log if needed
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
