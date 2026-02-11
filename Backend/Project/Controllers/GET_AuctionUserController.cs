using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Model;
using Project.Model.DTO;
using Project.Repository;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GET_AuctionUserController : ControllerBase
    {
        private readonly IGET_AuctionUserRepository repository;
        private readonly IMapper mapper;

        public GET_AuctionUserController(IGET_AuctionUserRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getall(GET_AuctionUserDomain input)
        {
            try
            {
                var domain = await repository.GetAll(input);
                var dto = mapper.Map<List<GET_AuctionUserDto>>(domain);

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
