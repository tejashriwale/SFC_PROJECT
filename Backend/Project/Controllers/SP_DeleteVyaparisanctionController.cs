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
    public class SP_DeleteVyaparisanctionController : ControllerBase
    {
        private readonly ISP_DeleteVyaparisanctionRepository repository;
        private readonly IMapper mapper;

        public SP_DeleteVyaparisanctionController(ISP_DeleteVyaparisanctionRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> get(SP_DeleteVyaparisanction input)
        {
            try
            {
                var domain = await repository.getasync(input);

                var dto = mapper.Map<List<ApiResponseMessageDto>>(domain);

                return Ok(dto);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
