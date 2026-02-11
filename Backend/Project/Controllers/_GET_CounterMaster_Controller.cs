using System.Drawing;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Project.Model.Domain;
using Project.Model.DTO;
using Project.Repository;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class _GET_CounterMaster_Controller : ControllerBase
    {
        private readonly I_GET_CounterMaster_Repository repository;
        private IMapper mapper;
        private readonly IConfiguration configuration;

        public _GET_CounterMaster_Controller(I_GET_CounterMaster_Repository repository, IMapper mapper, IConfiguration configuration)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> getasync(MasterInput input)
        {
            try
            {
                var domain = await repository.getasync(input);
                var dto = mapper.Map<List<_GET_CounterMaster_DTO>>(domain);

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

        public async Task<IActionResult> Get([FromBody] MasterInput input)
        {
            try
            {
                var domain = await repository.GetByGroupAsync(input);

                var dto = mapper.Map<List<_GET_CounterMaster_DTO>>(domain);

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
