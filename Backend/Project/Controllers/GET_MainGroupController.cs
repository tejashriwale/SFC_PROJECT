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
    public class GET_MainGroupController : ControllerBase
    {
        private readonly IGET_MainGroupRepository repository;
        private readonly IMapper mapper;

        public GET_MainGroupController(IGET_MainGroupRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync()
        {
            try
            {
                var domain = await repository.getasync();
                var dto = mapper.Map<List<GET_MainGroupDto>>(domain);

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
