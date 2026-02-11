using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Model.DTO;
using Project.Model;
using Project.Repository;
using Project.Model.Domain;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GET_VaypariNameController : ControllerBase
    {
        private readonly IGET_VaypariNameRepository repository;
        private readonly IMapper mapper;

        public GET_VaypariNameController(IGET_VaypariNameRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync(GET_VaypariNameDomain input)
        {
            try
            {
                var domain = await repository.getasync(input);
                var dto = mapper.Map<List<GET_VaypariNameDto>>(domain);

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
