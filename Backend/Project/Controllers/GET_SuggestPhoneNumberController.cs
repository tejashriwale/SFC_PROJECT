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
    public class GET_SuggestPhoneNumberController : ControllerBase
    {
        private readonly IGET_SuggestPhoneNumberRepository repository;
        private readonly IMapper mapper;

        public GET_SuggestPhoneNumberController(IGET_SuggestPhoneNumberRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> Get(GET_SuggestPhoneNumberDomain input)
        {
            try
            {
                var domain = await repository.getasync(input);
                var dto = mapper.Map<List<GET_SuggestPhoneNumberDto>>(domain);
                return Ok(domain);
            }
            catch (Exception ex)
            {
                // log if needed
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        [Route("_Search")]

        public async Task<IActionResult> getasync(GET_SuggestPhoneNumberDomain input)
        {
            try
            {
                var domain = await repository.getkeyword(input);

                var dto = mapper.Map<List<GET_SuggestPhoneNumberDto>>(domain);

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
