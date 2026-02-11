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
    public class GET_BOMPackingController : ControllerBase
    {
        private readonly IGET_BOMPackingRepository repository;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public GET_BOMPackingController(IGET_BOMPackingRepository repository, IMapper mapper, IConfiguration configuration)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> getasync(GET_BOMPackingDomain input)
        {
            try
            {
                var domain = await repository.getasync(input);
                var dto = mapper.Map<List<GET_BOMPackingDTO>>(domain);

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
        public async Task<IActionResult> Get([FromBody] GET_BOMPackingDomain input)
        {
            try
            {
                var domain = await repository.GetByGroupAsync(input);
                var dto = mapper.Map<List<GET_BOMPackingDTO>>(domain);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                // log if needed
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        [Route("_search")]

        public async Task<IActionResult> get([FromBody] GET_BOMPackingDomain input)
        {
            try
            {
                var domain = await repository.getkeyword(input);

                var dto = mapper.Map<List<GET_BOMPackingDTO>>(domain);

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
