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
    public class GET_AuctionDataBillController : ControllerBase
    {
        private readonly IGET_AuctionDataBillRepository repository;
        private readonly IMapper mapper;

        public GET_AuctionDataBillController(IGET_AuctionDataBillRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> AccessRightAllAsync(GET_AuctionDataBillDomain input)
        {
            try
            {
                var domain = await repository.getasync(input);

                var dto = mapper.Map<List<GET_AuctionDataBillDto>>(domain);

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
