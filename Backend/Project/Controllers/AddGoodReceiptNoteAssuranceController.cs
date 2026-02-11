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
    public class AddGoodReceiptNoteAssuranceController : ControllerBase
    {
        private readonly IAddGoodReceiptNoteAssuranceRepository repository;
        private readonly IMapper mapper;

        public AddGoodReceiptNoteAssuranceController(IAddGoodReceiptNoteAssuranceRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task <IActionResult>getasync(AddGoodReceiptNoteAssurance addGoodReceipt)
        {
            try
            {
                var domain = await repository.getasync(addGoodReceipt);
                var dto = mapper.Map<List<ApiResponseMessageDto>>(domain);

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
