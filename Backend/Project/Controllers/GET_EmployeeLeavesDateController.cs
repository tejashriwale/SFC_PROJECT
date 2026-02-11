using System.Diagnostics.Eventing.Reader;
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
    public class GET_EmployeeLeavesDateController : ControllerBase
    {
        private readonly IGET_EmployeeLeavesDateRepository repository;
        private readonly IMapper mapper;

        public GET_EmployeeLeavesDateController(IGET_EmployeeLeavesDateRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>getasync(GET_EmployeeLeavesDateDomain input)
        {
            try
            {
                var domain = await repository.getasync(input);
                var dto = mapper.Map<List<GET_EmployeeLeavesDateDto>>(domain);

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

        public async Task<IActionResult> Get([FromBody] GET_EmployeeLeavesDateDomain input)
        {
            try
            {
                var domain = await repository.getkeyword(input);

                var dto = mapper.Map<List<GET_EmployeeLeavesDateDto>>(domain);

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
