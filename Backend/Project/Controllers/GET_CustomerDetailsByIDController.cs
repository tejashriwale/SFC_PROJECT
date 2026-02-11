using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GET_CustomerDetailsByIDController : ControllerBase
    {
        private readonly IGET_CustomerDetailsByIDRepository repository;
        private readonly IMapper mapper;

        public GET_CustomerDetailsByIDController(IGET_CustomerDetailsByIDRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>getasync(CounterMasterDomain masterDomain)
        {
            try
            {
                var domain = await repository.getasync(masterDomain);
                var dto = mapper.Map<List<GET_CustomerDetailsByIDDto>>(domain);

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
