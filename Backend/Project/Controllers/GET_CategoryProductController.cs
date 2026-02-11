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
    public class GET_CategoryProductController : ControllerBase
    {
        private readonly IGET_CategoryProductRepository repository;
        private readonly IMapper mapper;

        public GET_CategoryProductController(IGET_CategoryProductRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task <IActionResult>getasync(GET_CategoryProductDomain customer)
        {
            try
            {
                var domain = await repository.getasync(customer);
                var dto = mapper.Map<List<GET_CategoryProductDTO>>(domain);

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
