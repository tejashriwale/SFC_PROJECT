using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Model.DTO;
using Project.Model.Model;
using Project.Repository;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WareHouseController : ControllerBase
    {
        private readonly IWareHouseRepository repository;
        private readonly IMapper mapper;

        public WareHouseController(IWareHouseRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task <IActionResult> WareHouseAsync([FromBody] WareHouseMaster wareHouse)
        {
            try
            {
                var domain = await repository.WareHouseAsync(wareHouse);

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
