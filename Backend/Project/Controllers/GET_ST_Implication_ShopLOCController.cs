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
    public class GET_ST_Implication_ShopLOCController : ControllerBase
    {
        private readonly IGET_ST_Implication_ShopLOCRepository repository;
        private readonly IMapper mapper;

        public GET_ST_Implication_ShopLOCController(IGET_ST_Implication_ShopLOCRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync(GET_ST_Implication_ShopLOC input)
        {
            try
            {
                var domain = await repository.GetAll();
                var dto = mapper.Map<List<GET_ST_Implication_ShopLOCDto>>(domain);

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
