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
    public class GET_PChallanMasterController : ControllerBase
    {
        private readonly IGET_PChallanMasterRepository repository;
        private readonly IMapper mapper;

        public GET_PChallanMasterController(IGET_PChallanMasterRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>getasync(GET_PChallanMasterDomain masterDomain)
        {
            try
            {
                var domain = await repository.getasync(masterDomain);
                var dto = mapper.Map<List<GET_PChallanMasterDto>>(domain);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                // log if needed
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        [Route("_Search")]

        public async Task<IActionResult> Get([FromBody] GET_PChallanMasterDomain masterDomain)
        {
            try
            {

                var domain = await repository.getkeyword(masterDomain);

                var dto = mapper.Map<List<GET_PChallanMasterDto>>(domain);

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
