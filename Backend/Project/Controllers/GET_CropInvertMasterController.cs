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
    public class GET_CropInvertMasterController : ControllerBase
    {
        private readonly IGET_CropInvertMasterRepository repository;
        private readonly IMapper mapper;

        public GET_CropInvertMasterController(IGET_CropInvertMasterRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync(GET_CropInvertMasterDomain category)
        {
            try
            {
                var domain = await repository.getasync(category);
                var dto = mapper.Map<List<GET_CropInvertMasterDto>>(domain);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                // log if needed
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        [Route("_Search")]

        public async Task<IActionResult> Get(GET_CropInvertMasterDomain input)
        {
            try
            {
                var domain = await repository.getasync(input);

                var dto = mapper.Map<List<GET_CropInvertMasterDto>>(domain);

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
