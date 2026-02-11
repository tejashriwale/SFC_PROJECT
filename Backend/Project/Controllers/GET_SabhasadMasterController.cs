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
    public class GET_SabhasadMasterController : ControllerBase
    {
        private readonly IGET_SabhasadMasterRepository repository;
        private readonly IMapper mapper;

        public GET_SabhasadMasterController(IGET_SabhasadMasterRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync(GET_SabhasadMasterDomain input)
        {
            var domain = await repository.getasync(input);
            var dto = mapper.Map<List<GET_SabhasadMasterDto>>(domain);

            return Ok(dto);
        }
        [HttpPost]
        [Route("_Search")]

        public async Task<IActionResult> Get(GET_SabhasadMasterDomain input)
        {
            var domain = await repository.getkeyword(input);

            var dto = mapper.Map<List<GET_SabhasadMasterDto>>(domain);

            return Ok(dto);
        }
    }
}
