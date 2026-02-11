using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Model.Domain;
using Project.Repository;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddGoodReceiptNoteMasterController : ControllerBase
    {
        private readonly IAddGoodReceiptNoteMasterRepository repository;
        private readonly IMapper mapper;

        public AddGoodReceiptNoteMasterController(IAddGoodReceiptNoteMasterRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>getasync(AddGoodReceiptNoteMaster addGoodReceipt)
        {
            try
            {
                var domain = await repository.getasync(addGoodReceipt);
                var dto = mapper.Map<List<ApiResponseMessage>>(domain);
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
