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
    public class HSNGSTDetailsController : ControllerBase
    {
        private readonly IHSNGSTDetailsRepository repository;
        private readonly IMapper mapper;

        public HSNGSTDetailsController(IHSNGSTDetailsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>getasync(HSNDOMAIN hSNDOMAIN)
        {
            try
            {
                var domain = await repository.getasync(hSNDOMAIN);

                var dto = mapper.Map<List<HSNGSTDetailsDTO>>(domain);

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
