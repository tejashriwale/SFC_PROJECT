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
    public class GET_VyapariBillChargesController : ControllerBase
    {
        private readonly IGET_VyapariBillChargesRepository repository;
        private readonly IMapper mapper;

        public GET_VyapariBillChargesController(IGET_VyapariBillChargesRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getall(GET_VyapariBillChargesDomain input)
        {
            try
            {
                var domain = await repository.getsync(input);
                var dto = mapper.Map<List<GET_VyapariBillChargesDto>>(domain);

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
