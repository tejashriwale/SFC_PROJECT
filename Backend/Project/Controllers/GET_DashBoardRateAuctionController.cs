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
    public class GET_DashBoardRateAuctionController : ControllerBase
    {
        private readonly IGET_DashBoardRateAuctionRepository repository;
        private readonly IMapper mapper;

        public GET_DashBoardRateAuctionController(IGET_DashBoardRateAuctionRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> get()
        {
            try
            {
                var domain = await repository.getall();
                var dto = mapper.Map<List<GET_DashBoardRateAuctionDto>>(domain);

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
