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
    public class GET_Cashier_BalanceController : ControllerBase
    {
        private readonly IGET_Cashier_BalanceRepository repository;
        private readonly IMapper mapper;

        public GET_Cashier_BalanceController(IGET_Cashier_BalanceRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> get( )
        {
            try
            {
                var domain = await repository.GetAll();

                var dto = mapper.Map<List<GET_Cashier_BalanceDto>>(domain);

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
