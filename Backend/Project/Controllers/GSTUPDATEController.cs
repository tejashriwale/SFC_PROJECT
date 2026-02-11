using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Model.Domain;
using Project.Repository;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GSTUPDATEController : ControllerBase
    {
        private readonly IGSTUPDATERepository repository;
        private readonly IMapper mapper;

        public GSTUPDATEController(IGSTUPDATERepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>get(GSTUPDATE gSTUPDATE)
        {
            try
            {
                var domain = await repository.getasync(gSTUPDATE);

                var dto = mapper.Map<List<ApiResponseMessage>>(domain);

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
