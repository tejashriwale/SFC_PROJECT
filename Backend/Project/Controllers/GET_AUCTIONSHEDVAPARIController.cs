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
    public class GET_AUCTIONSHEDVAPARIController : ControllerBase
    {
        private readonly IGET_AUCTIONSHEDVAPARIRepository repository;
        private readonly IMapper mapper;

        public GET_AUCTIONSHEDVAPARIController(IGET_AUCTIONSHEDVAPARIRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync(GET_AUCTIONSHEDVAPARIDomain input)
        {
            try
            {
                var domain = await repository.getasync(input);
                var dto = mapper.Map<List<GET_AUCTIONSHEDVAPARIDto>>(domain);

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
