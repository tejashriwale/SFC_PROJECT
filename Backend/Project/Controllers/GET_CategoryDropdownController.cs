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
    public class GET_CategoryDropdownController : ControllerBase
    {
        private readonly IGET_CategoryDropdownRepository repository;
        private readonly IMapper mapper;

        public GET_CategoryDropdownController(IGET_CategoryDropdownRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>getasync(GET_CategoryDropdownDomain input)
        {
            var domain = await repository.getasync(input);
            var dto = mapper.Map<List<GET_CategoryDropdownDto>>(domain);

            return Ok(dto);
        }
    }
}
