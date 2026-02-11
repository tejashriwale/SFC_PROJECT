using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Model.Domain;
using Project.Repository;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddUpdPBillMasterController : ControllerBase
    {
        private readonly IAddUpdPBillMasterRepository repository;
        private readonly IMapper mapper;

        public AddUpdPBillMasterController(IAddUpdPBillMasterRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>getasync(AddUpdPBillMaster pBillMaster)
        {
            try
            {
                var domain = await repository.getasync(pBillMaster);
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
