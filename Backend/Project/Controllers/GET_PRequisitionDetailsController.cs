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
    public class GET_PRequisitionDetailsController : ControllerBase
    {
        private readonly IGET_PRequisitionDetailsRepository repository;
        private readonly IMapper mapper;

        public GET_PRequisitionDetailsController(IGET_PRequisitionDetailsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult>getasync(RepuisitonDomain input)
        {
            var domain = await repository.getasync(input);
            var dto = mapper.Map<List<GET_PRequisitionDetailsDto>>(domain);

            return Ok(dto);
        }
    }
}
