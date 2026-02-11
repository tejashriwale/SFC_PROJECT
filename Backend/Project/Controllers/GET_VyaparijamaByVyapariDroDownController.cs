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
    public class GET_VyaparijamaByVyapariDroDownController : ControllerBase
    {
        private readonly IGET_VyaparijamaByVyapariDroDownRepository repository;
        private readonly IMapper mapper;

        public GET_VyaparijamaByVyapariDroDownController(IGET_VyaparijamaByVyapariDroDownRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> get(GET_VyaparijamaByVyapariDroDownDomain input)
        {
            try
            {
                var domain = await repository.getasync(input);
                var dto = mapper.Map<List<GET_VyaparijamaByVyapariDroDownDto>>(domain);

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
