using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Project.Model.DTO;
using Project.Repository;
using System.Drawing;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        private readonly IMarkSheetRepository markSheetRepository;
        private readonly IMapper mapper;

        public ExcelController(IMarkSheetRepository markSheetRepository, IMapper mapper)
        {
            this.markSheetRepository = markSheetRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var domain = await markSheetRepository.GetAllAsyn();
            var dto = mapper.Map<List<MarkSheetDto>>(domain);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("MarkSheet");
                worksheet.Cells.LoadFromCollection(dto, true);
                package.Save();
            }

            stream.Position = 0;
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = "MarkSheet.xlsx";

            return File(stream, contentType, fileName);
        }
    }
}
