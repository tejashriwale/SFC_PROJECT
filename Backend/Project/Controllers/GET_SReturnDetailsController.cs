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
    public class GET_SReturnDetailsController : ControllerBase
    {
        private readonly IGET_SReturnDetailsRepository repository;
        private readonly IMapper mapper;

        public GET_SReturnDetailsController(IGET_SReturnDetailsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>getasync(GET_SReturnDetailsDomain input)
        {
            var domain = await repository.getasync(input);
            var dto = mapper.Map<List<GET_SReturnDetailsDto>>(domain);

            return Ok(dto);
        }
    }
}
