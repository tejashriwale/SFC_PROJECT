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
    public class SP_UpadateUserLoginController : ControllerBase
    {
        private readonly ISP_UpadateUserLoginRepository repository;
        private readonly IMapper mapper;

        public SP_UpadateUserLoginController(ISP_UpadateUserLoginRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync(SP_UpadateUserLogin input)
        {
            try
            {
                // When authenticated, a caller may only change their own login state
                var tokenUaid = User.FindFirst("uaid")?.Value;
                if (User.Identity?.IsAuthenticated == true && !string.IsNullOrEmpty(tokenUaid) && tokenUaid != input?.UAID?.ToString())
                    return Forbid();

                var domain = await repository.getasync(input);
                var dto = mapper.Map<List<ApiResponseMessageDto>>(domain);

                return Ok(dto);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
