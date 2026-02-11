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
    public class GET_QuatationMasterSearchController : ControllerBase
    {
        private readonly IGET_QuatationMasterSearchRepository repository;
        private readonly IMapper mapper;

        public GET_QuatationMasterSearchController(IGET_QuatationMasterSearchRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>getasync(GET_QuatationMasterSearchDomain gET_Quatation)
        {
            try
            {
                var domain = await repository.getasync(gET_Quatation);
                var dto = mapper.Map<List<GET_QuatationMasterSearchDto>>(domain);

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
