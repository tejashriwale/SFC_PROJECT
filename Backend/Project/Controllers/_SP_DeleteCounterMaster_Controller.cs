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
    public class _SP_DeleteCounterMaster_Controller : ControllerBase
    {
        private readonly I_SP_DeleteCounterMaster_Repository repository;
        private readonly IMapper mapper;

        public _SP_DeleteCounterMaster_Controller(I_SP_DeleteCounterMaster_Repository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> getasync([FromBody] _SP_DeleteCounterMaster_ input)
        {
            try
            {
                var domain = await repository.getasync(input);

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
