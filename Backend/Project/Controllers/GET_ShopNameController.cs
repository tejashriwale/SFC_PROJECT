using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Model.Domain;
using Project.Model.DTO;
using Project.Repository;
using Project.Service; // Make sure IJwtTokenService is defined in this namespace

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GET_ShopNameController : ControllerBase
    {
        private readonly IGET_ShopNameRepository repository;
        private readonly IMapper mapper;
        //private readonly IJwtTokenService jwtTokenService;

        public GET_ShopNameController(IGET_ShopNameRepository repository, IMapper mapper/*, IJwtTokenService jwtTokenService*/)
        {
            this.repository = repository;
            this.mapper = mapper;
            //this.jwtTokenService = jwtTokenService;
        }

        [HttpPost]
        public async Task<IActionResult> GetAsync(GET_ShopNameDomain input)
        {
            try
            {
                var domain = await repository.getasync(input);
                var dto = mapper.Map<List<GET_ShopNameDto>>(domain);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

       
    }
}
