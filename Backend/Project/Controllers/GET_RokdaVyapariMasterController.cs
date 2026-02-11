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
    public class GET_RokdaVyapariMasterController : ControllerBase
    {
        private readonly IGET_RokdaVyapariMasterRepository repository;
        private readonly IMapper mapper;

        public GET_RokdaVyapariMasterController(IGET_RokdaVyapariMasterRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync(GET_RokdaVyapariMasterDomain input)
        {
            var domain = await repository.getasync(input);
            var dto = mapper.Map<List<GET_RokdaVyapariMasterDto>>(domain);

            return Ok(dto);
        }
        [HttpPost]
        [Route("_Search")]

        public async Task<IActionResult> Get([FromBody] GET_RokdaVyapariMasterDomain input)
        {
            var domain = await repository.getkeyword(input);

            var dto = mapper.Map<List<GET_RokdaVyapariMasterDto>>(domain);

            return Ok(dto);
        }
    }
}
