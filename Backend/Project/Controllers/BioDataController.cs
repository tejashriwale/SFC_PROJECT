using Aspose.Words;
using Aspose.Words.Replacing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using System.IO;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BioDataController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public BioDataController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost("generate-pdf")]
        public IActionResult GeneratePdf([FromBody] BioDataDto data)
        {
            var templatePath = Path.Combine(_env.WebRootPath, "BioData2.docx");

            if (!System.IO.File.Exists(templatePath))
            {
                return NotFound("Template file not found at: " + templatePath);
            }

            var doc = new Document(templatePath);

            doc.Range.Replace("{{FullName}}", data.FullName ?? "", new FindReplaceOptions());
            doc.Range.Replace("{{Birthdate}}", data.Birthdate ?? "", new FindReplaceOptions());
            doc.Range.Replace("{{Height}}", data.Height ?? "", new FindReplaceOptions());
            doc.Range.Replace("{{Weight}}", data.Weight ?? "", new FindReplaceOptions());
            doc.Range.Replace("{{BodyType}}", data.BodyType ?? "", new FindReplaceOptions());
            doc.Range.Replace("{{BodyColor}}", data.BodyColor ?? "", new FindReplaceOptions());
            doc.Range.Replace("{{Religion}}", data.Religion ?? "", new FindReplaceOptions());
            doc.Range.Replace("{{Caste}}", data.Caste ?? "", new FindReplaceOptions());
            doc.Range.Replace("{{Education}}", data.Education ?? "", new FindReplaceOptions());
            doc.Range.Replace("{{Designation}}", data.Designation ?? "", new FindReplaceOptions());
            doc.Range.Replace("{{WorkLocation}}", data.WorkLocation ?? "", new FindReplaceOptions());
            doc.Range.Replace("{{AnnualIncome}}", data.AnnualIncome ?? "", new FindReplaceOptions());
            doc.Range.Replace("{{Hobbies}}", data.Hobbies ?? "", new FindReplaceOptions());
            doc.Range.Replace("{{Sports}}", data.Sports ?? "", new FindReplaceOptions());
            doc.Range.Replace("{{FatherName}}", data.FatherName ?? "", new FindReplaceOptions());
            doc.Range.Replace("{{MotherName}}", data.MotherName ?? "", new FindReplaceOptions());
            doc.Range.Replace("{{FatherOccupation}}", data.FatherOccupation ?? "", new FindReplaceOptions());
            doc.Range.Replace("{{MotherOccupation}}", data.MotherOccupation ?? "", new FindReplaceOptions());
            doc.Range.Replace("{{FamilyStatus}}", data.FamilyStatus ?? "", new FindReplaceOptions());
            doc.Range.Replace("{{FamilyType}}", data.FamilyType ?? "", new FindReplaceOptions());
            doc.Range.Replace("{{FamilyValue}}", data.FamilyValue ?? "", new FindReplaceOptions());
            doc.Range.Replace("{{MobileNo}}", data.MobileNo ?? "", new FindReplaceOptions());
            doc.Range.Replace("{{Address}}", data.Address ?? "", new FindReplaceOptions());

            using var stream = new MemoryStream();
            doc.Save(stream, SaveFormat.Pdf);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "Biodata-Filled.pdf");
        }
    }
}
