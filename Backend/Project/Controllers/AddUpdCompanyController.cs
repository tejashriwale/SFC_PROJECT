using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Model.Domain;
using Project.Repository;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddUpdCompanyController : ControllerBase
    {
        private readonly IAddUpdCompanyRepository repository;
        private readonly IMapper mapper;

        public AddUpdCompanyController(IAddUpdCompanyRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task <IActionResult>getasync(AddUpdCompany addUpdCompany)
        {
            try
            {
                var domain = await repository.getassync(addUpdCompany);
                var dto = mapper.Map<List<ApiResponseMessage>>(domain);

                return Ok(dto);
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
