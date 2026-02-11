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
    public class GET_CreateFarmerBillVyapariController : ControllerBase
    {
        private readonly IGET_CreateFarmerBillVyapariRepository repository;
        private readonly IMapper mapper;

        public GET_CreateFarmerBillVyapariController(IGET_CreateFarmerBillVyapariRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync(GET_CreateFarmerBillVyapariDomain category)
        {
            try
            {
                var domain = await repository.getasync(category);
                var dto = mapper.Map<List<GET_CreateFarmerBillVyapariDto>>(domain);

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
