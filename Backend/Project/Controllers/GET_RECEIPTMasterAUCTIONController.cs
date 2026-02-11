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
    public class GET_RECEIPTMasterAUCTIONController : ControllerBase
    {
        private readonly IGET_RECEIPTMasterAUCTIONRepository repository;
        private readonly IMapper mapper;

        public GET_RECEIPTMasterAUCTIONController(IGET_RECEIPTMasterAUCTIONRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync(GET_RECEIPTMasterAUCTIONDomain input)
        {
            try
            {
                var domain = await repository.getasync(input);
                var dto = mapper.Map<List<GET_RECEIPTMasterAUCTIONDto>>(domain);

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
