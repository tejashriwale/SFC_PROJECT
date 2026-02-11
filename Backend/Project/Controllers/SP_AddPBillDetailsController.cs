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
    public class SP_AddPBillDetailsController : ControllerBase
    {
        private readonly ISP_AddPBillDetailsReposioty reposioty;
        private readonly IMapper mapper;

        public SP_AddPBillDetailsController(ISP_AddPBillDetailsReposioty reposioty, IMapper mapper)
        {
            this.reposioty = reposioty;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> AddMultipleAsync(List<SP_AddPBillDetails> input)
        {
            try
            {
                var domain = await reposioty.getasync(input);
                var dto = mapper.Map<List<ApiResponseMessageDto>>(domain);

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
