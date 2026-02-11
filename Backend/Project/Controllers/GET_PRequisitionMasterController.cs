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
    public class GET_PRequisitionMasterController : ControllerBase
    {
        private readonly IGET_PRequisitionMasterRepositroy repositroy;
        private readonly IMapper mapper;

        public GET_PRequisitionMasterController(IGET_PRequisitionMasterRepositroy repositroy, IMapper mapper)
        {
            this.repositroy = repositroy;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task <IActionResult>getasync(GET_PRequisitionMasterDomain input)
        {
            try
            {
                var domain = await repositroy.getasync(input);
                var dto = mapper.Map<List<GET_PRequisitionMasterDTO>>(domain);

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

        public async Task<IActionResult> Get([FromBody] GET_PRequisitionMasterDomain input)
        {
            try
            {
                var domain = await repositroy.getkeyword(input);

                var dto = mapper.Map<List<GET_PRequisitionMasterDTO>>(domain);

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
