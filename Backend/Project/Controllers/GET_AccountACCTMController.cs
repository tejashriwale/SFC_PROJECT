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
    public class GET_AccountACCTMController : ControllerBase
    {
        private readonly IGET_AccountACCTMRepository repository;
        private readonly IMapper mapper;

        public GET_AccountACCTMController(IGET_AccountACCTMRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync( )
        {
            try
            {
                var domain = await repository.GetAll();
                var dto = mapper.Map<List<GET_AccountACCTMDto>>(domain);

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
