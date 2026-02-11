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
    public class GET_CropVehicleLoadingDataController : ControllerBase
    {
        private readonly IGET_CropVehicleLoadingDataRepository repository;
        private readonly IMapper mapper;

        public GET_CropVehicleLoadingDataController(IGET_CropVehicleLoadingDataRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync(GET_CropVehicleLoadingDataDomain category)
        {
            try
            {
                var domain = await repository.getasync(category);
                var dto = mapper.Map<List<GET_CropVehicleLoadingDataDto>>(domain);

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
