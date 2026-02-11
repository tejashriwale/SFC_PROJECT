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
    public class GET_PurchaseOrderDataByIdController : ControllerBase
    {
        private readonly IGET_PurchaseOrderDataByIdRepository repository;
        private readonly IMapper mapper;

        public GET_PurchaseOrderDataByIdController(IGET_PurchaseOrderDataByIdRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>getasync(GET_PurchaseOrderDataByIdDomain input)
        {
            try
            {
                var domain = await repository.getasync(input);
                var dto = mapper.Map<List<GET_PurchaseOrderDataByIdDto>>(domain);

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
