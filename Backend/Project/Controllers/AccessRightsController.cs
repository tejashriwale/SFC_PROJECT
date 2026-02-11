using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Model.Domain;
using Project.Repository;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessRightsController : ControllerBase
    {
        private readonly IAccessRightsRepository repository;
        private readonly IMapper mapper;    

        public AccessRightsController(IAccessRightsRepository repository, IMapper mapper)
        {
            this.repository = repository;   
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>getasync(AccessRights accessRights)
        {
            try
            {
                var domain = await repository.getasync(accessRights);

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
