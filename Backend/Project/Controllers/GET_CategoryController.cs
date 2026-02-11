using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Model.Domain;
using Project.Repository;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class GET_CategoryController : ControllerBase
    {
        private readonly IGET_CategoryRepository repository;
        private readonly IMapper mapper;
        //private readonly IJwtTokenService jwtTokenService;

        public GET_CategoryController(IGET_CategoryRepository repository, IMapper mapper/*, IJwtTokenService jwtTokenService*/)
        {
            this.repository = repository;
            this.mapper = mapper;
            //this.jwtTokenService = jwtTokenService;
        }
        [HttpPost]

        public async Task <IActionResult>getasync(GET_CategoryDomain category)
        {
            try
            {
                var domain = await repository.getasync(category);
                var dto = mapper.Map<List<GET_Category>>(domain);

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
