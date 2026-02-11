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
    public class _GET_HSNMasters_Controller : ControllerBase
    {
        private readonly I_GET_HSNMasters_Repository repository;
        private readonly IMapper mapper;

        public _GET_HSNMasters_Controller(I_GET_HSNMasters_Repository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync(DomainMaster input)
        {
            try
            {
                var domain = await repository.getasync(input);
                var dto = mapper.Map<List<_GET_HSNMasters_DTO>>(domain);

                return Ok(dto);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "An error occurred while uploading the advertisement.",
                    error = ex.Message
                });
            }
        }
        [HttpPost]
        [Route("getById/")]

        public async Task<IActionResult> Get([FromBody] DomainMaster input)
        {
            try
            {
                var domain = await repository.GetByGroupAsync(input);

                var dto = mapper.Map<List<_GET_HSNMasters_DTO>>(domain);

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

        public async Task<IActionResult> sarchasync([FromBody] DomainMaster input)
        {
            try
            {
                var domain = await repository.Serchasync(input);

                var dto = mapper.Map<List<_GET_HSNMasters_DTO>>(domain);

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
