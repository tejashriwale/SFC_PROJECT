using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Model.DTO;
using Project.Repository;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GET_CompanyDropdownController : ControllerBase
    {
        private readonly IGET_CompanyDropdownRepository repository;
        private readonly IMapper mapper;

        public GET_CompanyDropdownController(IGET_CompanyDropdownRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>getall()
        {
            try
            {
                var domain = await repository.getall();
                var dto = mapper.Map<List<GET_CompanyDropdownDto>>(domain);

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
