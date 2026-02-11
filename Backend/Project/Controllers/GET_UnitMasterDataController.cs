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
    public class GET_UnitMasterDataController : ControllerBase
    {
        private readonly IGET_UnitMasterDataRepository repository;
        private readonly IMapper mapper;

        public GET_UnitMasterDataController(IGET_UnitMasterDataRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>get()
        {
            try
            {
                var domain = await repository.GetAll();
                var dto = mapper.Map<List<GET_UnitMasterDataDto>>(domain);

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
