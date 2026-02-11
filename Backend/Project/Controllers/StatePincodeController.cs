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
    public class StatePincodeController : ControllerBase
    {
        private readonly IStatePincodeRepository repository;
        private readonly IMapper mapper;

        public StatePincodeController(IStatePincodeRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>getasync(StatePincodeDomain statePincodeDomain)
        {
            try
            {
                var domain = await repository.getasync(statePincodeDomain);
                var dto = mapper.Map<List<StatePincodeDTO>>(domain);

                return Ok(dto);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
