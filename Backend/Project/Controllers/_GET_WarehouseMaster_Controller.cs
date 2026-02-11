using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Model;
using Project.Model.Domain;
using Project.Model.DTO;
using Project.Repository;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class _GET_WarehouseMaster_Controller : ControllerBase
    {
        private readonly I_GET_WarehouseMaster_Repository repository;
        private readonly IMapper mapper;

        public _GET_WarehouseMaster_Controller(I_GET_WarehouseMaster_Repository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>getasync(getByIdInput input)
        {
            try
            {
                var domain = await repository.getasync(input);
                var dto = mapper.Map<List<_GET_WarehouseMaster_DTO>>(domain);

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

        public async Task<IActionResult> Get([FromBody] getByIdInput input)
        {
            try
            {
                var domain = await repository.GetByGroupAsync(input);

                var dto = mapper.Map<List<_GET_WarehouseMaster_DTO>>(domain);

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

        public async Task<IActionResult> Serchasync([FromBody] getByIdInput input)
        {
            try
            {
                var domain = await repository.searchasync(input);

                var dto = mapper.Map<List<_GET_WarehouseMaster_DTO>>(domain);

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
