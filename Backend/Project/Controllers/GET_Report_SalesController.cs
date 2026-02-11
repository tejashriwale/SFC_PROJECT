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
    public class GET_Report_SalesController : ControllerBase
    {
        private readonly IGET_Report_SalesRepository repository;
        private readonly IMapper mapper;

        public GET_Report_SalesController(IGET_Report_SalesRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost]

        public async Task<IActionResult> getasync(GET_Report_SalesDomain input)
        {
            var domain = await repository.getasync(input);
            var dto = mapper.Map<List<GET_Report_SalesDto>>(domain);

            return Ok(dto);
        }
    }
}
