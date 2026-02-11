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
    public class _GET_CustomersPartners_Controller : ControllerBase
    {
        private readonly IGET_CustomersPartnersRepository repository;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public _GET_CustomersPartners_Controller(IGET_CustomersPartnersRepository repository, IMapper mapper, IConfiguration configuration)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.configuration = configuration;
        }
        [HttpPost]

        public async Task<IActionResult> getasync(CustomerDomain input)
        {
            try
            {
                var domain = await repository.getasync(input);
                var dto = mapper.Map<List<GET_CustomersPartnersDto>>(domain);

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
        [Route("getByID/")]
        public async Task<IActionResult> Get([FromBody] CustomerDomain input)
        {
            try
            {
                var domain = await repository.GetByGroupAsync(input);
                var dto = mapper.Map<List<GET_CustomersPartnersDto>>(domain);

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
        public async Task<IActionResult> Serchasync([FromBody] CustomerDomain input)
        {
            try
            {
                var domain = await repository.searchasync(input);
                var dto = mapper.Map<List<GET_CustomersPartnersDto>>(domain);

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
