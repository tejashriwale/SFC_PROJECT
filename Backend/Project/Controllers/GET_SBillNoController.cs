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
    public class GET_SBillNoController : ControllerBase
    {
        private readonly IGET_SBillNoRepository repository;
        private readonly IMapper mapper;

        public GET_SBillNoController(IGET_SBillNoRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync(GET_SBillNoDomain input)
        {
            var domain = await repository.getasync(input);
            var dto = mapper.Map<List<GET_SBillNoDto>>(domain);

            return Ok(dto);
        }
    }
}
