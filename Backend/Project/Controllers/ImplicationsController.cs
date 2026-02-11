using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Model.DTO;
using Project.Repository;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImplicationsController : ControllerBase
    {
        private readonly IImplicationsRepository repository;
        private readonly IMapper mapper;

        public ImplicationsController(IImplicationsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpGet]

        public async Task<IActionResult> GetAllAsync()
        {
            var domain = await repository.GetAllAsync();
            var dto = mapper.Map<List<ImplicationsDTO>>(domain);

            return Ok(dto);
        }

        [HttpGet]
        [Route("{groupIDs}")]

        public async Task<IActionResult> GetByGroups([FromRoute] string groupIDs)
        {
            var domain = await repository.GetByGroupAsync(groupIDs);
            var dto = mapper.Map<List<ImplicationsDTO>>(domain);

            return Ok(dto);
        }
    }
}
