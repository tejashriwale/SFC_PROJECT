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
    public class GET_CropDirectInvertMasterController : ControllerBase
    {
        private readonly IGET_CropDirectInvertMasterRepository repository;
        private readonly IMapper mapper;

        public GET_CropDirectInvertMasterController(IGET_CropDirectInvertMasterRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync(GET_CropDirectInvertMasterDomain category)
        {
            try
            {
                var domain = await repository.getasync(category);
                var dto = mapper.Map<List<GET_CropDirectInvertMasterDto>>(domain);

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

        public async Task<IActionResult> Get(GET_CropDirectInvertMasterDomain input)
        {
            try
            {
                var domain = await repository.getasync(input);

                var dto = mapper.Map<List<GET_CropDirectInvertMasterDto>>(domain);

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
