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
    public class GET_AccessRights_Controller : ControllerBase
    {
        private readonly I_GET_AccessRights_Repository repository;
        private readonly IMapper mapper;

        public GET_AccessRights_Controller(I_GET_AccessRights_Repository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> AccessRightAllAsync()
        {
            try
            {

                var domain = await repository.AccessRightAllAsync();

                var dto = mapper.Map<List<_GET_AccessRights_Dto>>(domain);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                // log if needed
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("getByID/")]

        public async Task<IActionResult> Get([FromBody] _GET_AccessRights_Domain input)
        {
            try
            {
                var domain = await repository.GetByGroupAsync(input);

                var dto = mapper.Map<List<_GET_AccessRights_Dto>>(domain);

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
