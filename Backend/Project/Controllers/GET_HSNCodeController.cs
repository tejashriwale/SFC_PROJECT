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
    public class GET_HSNCodeController : ControllerBase
    {
        private readonly IGET_HSNCodeRepository repository;
        private readonly IMapper mapper;

        public GET_HSNCodeController(IGET_HSNCodeRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> getasync(HSNCodeDomain hSNCode)
        {
            try
            {
                var domain = await repository.getasync(hSNCode);
                var dto = mapper.Map<List<GET_HSNCodeDTO>>(domain);

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
