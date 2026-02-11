using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Model.Domain;
using Project.Repository;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddUpdPChallanDetailsController : ControllerBase
    {
        private readonly IAddUpdPChallanDetailsRepository repository;
        private readonly IMapper mapper;

        public AddUpdPChallanDetailsController(IAddUpdPChallanDetailsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> AddMultipleAsync([FromBody] List<AddUpdPChallanDetails> pChallanDetails)
        {
            try
            {
                var domain = await repository.getasync(pChallanDetails);
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
