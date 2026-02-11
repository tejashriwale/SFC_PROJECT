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
    public class GET_SaveNotificationController : ControllerBase
    {
        private readonly IGET_SaveNotificationRepository repository;
        private readonly IMapper mapper;

        public GET_SaveNotificationController(IGET_SaveNotificationRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync(GET_SaveNotificationDomain input)
        {
            try
            {
                var domain = await repository.getasync(input);
                var dto = mapper.Map<List<GET_SaveNotificationDto>>(domain);

                return Ok(dto);
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
