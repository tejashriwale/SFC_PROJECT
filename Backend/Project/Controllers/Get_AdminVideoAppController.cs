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
    public class Get_AdminVideoAppController : ControllerBase
    {
        private readonly IGet_AdminVideoAppRepository repository;
        private readonly IMapper mapper;

        public Get_AdminVideoAppController(IGet_AdminVideoAppRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var domain = await repository.GetAll();

                var dto = mapper.Map<List<Get_AdminVideoAppDto>>(domain);

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
