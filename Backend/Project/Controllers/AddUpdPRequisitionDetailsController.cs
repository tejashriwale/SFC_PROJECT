using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Model.Domain;
using Project.Repository;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddUpdPRequisitionDetailsController : ControllerBase
    {
        private readonly IAddUpdPRequisitionDetailsRepository repository;
        private readonly IMapper mapper;

        public AddUpdPRequisitionDetailsController(IAddUpdPRequisitionDetailsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]
      
        public async Task<IActionResult> AddMultipleAsync([FromBody] List<AddUpdPRequisitionDetails> pRequisitionDetailsList)
        {
            try
            {
                var domain = await repository.getasync(pRequisitionDetailsList);
                var dto = mapper.Map<List<ApiResponseMessage>>(domain);

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
