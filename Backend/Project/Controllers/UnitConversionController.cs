using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Model.Domain;
using Project.Repository;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitConversionController : ControllerBase
    {
        private readonly IUnitConversionRepository repository;
        private readonly IMapper mapper;

        public UnitConversionController(IUnitConversionRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpPost]

        public async Task<IActionResult> getasync(UnitConversion unitConversion)
        {
            try
            {
                var domain = await repository.getasync(unitConversion);

                var dto = mapper.Map<List<ApiResponseMessage>>(domain);

                return Ok(dto);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
 

    

    
    

    
}
