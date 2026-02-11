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
    public class GET_FarmerPaymentSummaryController : ControllerBase
    {
        private readonly IGET_FarmerPaymentSummaryRepository repository;
        private readonly IMapper mapper;

        public GET_FarmerPaymentSummaryController(IGET_FarmerPaymentSummaryRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> getasync(GET_FarmerPaymentSummaryDomain input)
        {
            try
            {
                var domain = await repository.getasync(input);
                var dto = mapper.Map<List<GET_FarmerPaymentSummaryDto>>(domain);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("UpdatePaymentDate")]
        public async Task<IActionResult> UpdatePaymentDate(UpdatePaymentDateDomain input)
        {
            try
            {
                var result = await repository.UpdatePaymentDateAsync(input);

                if (result)
                {
                    return Ok(new { success = true, message = "Payment date updated successfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Failed to update payment date" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
