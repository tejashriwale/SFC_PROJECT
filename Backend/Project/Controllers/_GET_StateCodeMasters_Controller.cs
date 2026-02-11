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
    public class _GET_StateCodeMasters_Controller : ControllerBase
    {
        private readonly I_GET_StateCodeMasters_Repository repository;
        private readonly IMapper mapper;

        public _GET_StateCodeMasters_Controller(I_GET_StateCodeMasters_Repository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>getasync (DomainMaster input)
        {
            try
            {
                var domain = await repository.getasync(input);
                var dto = mapper.Map<List<_GET_StateCodeMasters_DTO>>(domain);

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

        public async Task<IActionResult> Get([FromBody] DomainMaster input)
        {
            try
            {
                var domain = await repository.GetByGroupAsync(input);

                var dto = mapper.Map<List<_GET_StateCodeMasters_DTO>>(domain);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                // log if needed
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        [Route("Serch/")]
        public async Task<IActionResult> Serchasync([FromBody] DomainMaster input)
        {
            try
            {
                var domain = await repository.serchasync(input);

                var dto = mapper.Map<List<_GET_StateCodeMasters_DTO>>(domain);

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
