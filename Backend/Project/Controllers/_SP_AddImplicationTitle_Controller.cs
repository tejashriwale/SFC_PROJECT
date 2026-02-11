using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Model.Domain;
using Project.Repository;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class _SP_AddImplicationTitle_Controller : ControllerBase
    {
        private readonly I_SP_AddImplicationTitle_Repository repository;
        private readonly IMapper mapper;

        public _SP_AddImplicationTitle_Controller(I_SP_AddImplicationTitle_Repository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync(_SP_AddImplicationTitle_ sP_AddImplicationTitle_)
        {
            var domain = await repository.getasync(sP_AddImplicationTitle_);
            var dto = mapper.Map<List<ApiResponseMessage>>(domain);

            return Ok(dto);
        }
    }
}
