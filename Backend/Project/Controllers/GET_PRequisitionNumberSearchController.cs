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
    public class GET_PRequisitionNumberSearchController : ControllerBase
    {
        private readonly IGET_PRequisitionNumberSearchRepository repository;
        private readonly IMapper mapper;

        public GET_PRequisitionNumberSearchController(IGET_PRequisitionNumberSearchRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync(GET_PRequisitionNosearchDomain gET_PRequisition)
        {
            try
            {
                var domain = await repository.getasync(gET_PRequisition);
                var dto = mapper.Map<List<GET_PRequisitionNumberSearchDTO>>(domain);

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
