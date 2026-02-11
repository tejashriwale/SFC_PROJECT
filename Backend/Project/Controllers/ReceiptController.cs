using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Model;
using Project.Model.Domain;
using Project.Model.DTO;
using Project.Repository;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceiptController : ControllerBase
    {
        private readonly IReceiptRepository Repository;

        public ReceiptController(IReceiptRepository repository)
        {
            Repository = repository;
        }

        [HttpPost("add-receipt")]
        public async Task<IActionResult> AddReceipt([FromBody] ReceiptRequest request)
        {
            if (!ModelState.IsValid || request == null || request.ReceiptMaster == null)
                return BadRequest(ModelState);

            var result = await Repository.AddOrUpdateReceiptAsync(request);
            return result ? Ok("Success") : BadRequest("Failed");
        }
    }


}



