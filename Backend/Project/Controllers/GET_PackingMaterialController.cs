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
    public class GET_PackingMaterialController : ControllerBase
    {
        private readonly IGET_PackingMaterialRepository repository;
        private readonly IMapper mapper;

        public GET_PackingMaterialController(IGET_PackingMaterialRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync(GET_PackingMaterialDomain input)
        {
            var domain = await repository.getasync(input);
            var dto = mapper.Map<List<GET_PackingMaterialDto>>(domain);

            return Ok(dto);
        }
        [HttpPost]
        [Route("_Search")]

        public async Task<IActionResult> Get(GET_PackingMaterialDomain input)
        {
            try
            {
                var domain = await repository.getkeyword(input);

                var dto = mapper.Map<List<GET_PackingMaterialDto>>(domain);

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
