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
    public class SP_AddUpdVyapariBillMasterSaveAuctionController : ControllerBase
    {
        private readonly ISP_AddUpdVyapariBillMasterSaveAuctionRepository repository;
        private readonly IMapper mapper;

        public SP_AddUpdVyapariBillMasterSaveAuctionController(ISP_AddUpdVyapariBillMasterSaveAuctionRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync([FromBody]List<SP_AddUpdVyapariBillMasterSaveAuction> input)
        {
            try
            {
                var domain = await repository.getasync(input);
                var dto = mapper.Map<List<ApiResponseMessageDto>>(domain);

                return Ok(dto);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
