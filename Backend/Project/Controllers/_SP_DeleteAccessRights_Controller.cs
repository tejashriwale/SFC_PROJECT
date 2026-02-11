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
    public class _SP_DeleteAccessRights_Controller : ControllerBase
    {
        private readonly I_SP_DeleteAccessRights_Repository repository;
        private readonly IMapper mapper;

        public _SP_DeleteAccessRights_Controller(I_SP_DeleteAccessRights_Repository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> getasync([FromBody] _SP_DeleteAccessRights_ sP_DeleteAccessRights_)
        {
            try
            {
                var domain = await repository.getasync(sP_DeleteAccessRights_);
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
