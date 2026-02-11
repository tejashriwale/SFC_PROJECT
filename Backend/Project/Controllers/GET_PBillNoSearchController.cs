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
    public class GET_PBillNoSearchController : ControllerBase
    {
        private readonly IGET_PBillNoSearchRepository repositroy;
        private readonly IMapper mapper;

        public GET_PBillNoSearchController(IGET_PBillNoSearchRepository repositroy, IMapper mapper)
        {
            this.repositroy = repositroy;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>getasync(GET_PBillNoSearchDomain input)
        {
            try
            {
                var domain = await repositroy.getasync(input);
                var dto = mapper.Map<List<GET_PBillNoSearchDto>>(domain);

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
