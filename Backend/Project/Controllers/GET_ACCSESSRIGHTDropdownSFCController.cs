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
    public class GET_ACCSESSRIGHTDropdownSFCController : ControllerBase
    {
        private readonly IGET_ACCSESSRIGHTDropdownSFCRepository repository;
        private readonly IMapper mapper;

        public GET_ACCSESSRIGHTDropdownSFCController(IGET_ACCSESSRIGHTDropdownSFCRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> getasync(GET_ACCSESSRIGHTDropdownSFCDomain input)
        {
            try
            {
                var domain = await repository.getasync(input);

                var dto = mapper.Map<List<GET_ACCSESSRIGHTDropdownSFCDto>>(domain);

                return Ok(dto);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
