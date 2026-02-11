
using matrimonybackendApi.Models.Domain;
using matrimonybackendApi.Models.DTO;
using Project.Data;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;

namespace matrimonybackendApi.Repositories
{
    public class SqlPdfUploadRepository : IPdfUploadRepository
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _dbContext;

        public SqlPdfUploadRepository(IWebHostEnvironment environment, ApplicationDbContext dbContext)
        {
            _environment = environment;
            _dbContext = dbContext;
        }

        public async Task<PdfUpload> UploadAsync(PdfUpload pdf)
        {
            // Change path to "Images" folder
            var rootPath = Path.Combine(_environment.WebRootPath, "Images");  // Change here to "Images"
            if (!Directory.Exists(rootPath)) Directory.CreateDirectory(rootPath);  // Ensure the Images folder exists

            var ext = Path.GetExtension(pdf.File.FileName);
            var fileName = $"{Path.GetFileNameWithoutExtension(pdf.FileName)}_{DateTime.Now.Ticks}{ext}";
            var fullPath = Path.Combine(rootPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await pdf.File.CopyToAsync(stream);
            }

            pdf.FilePath = $"/Images/{fileName}";  // Saving file path in the Images folder
           

            return pdf;
        }

        public async Task<PdfUpload> MergeAsync(PdfMergeDto dto)
        {
            var rootPath = Path.Combine(_environment.WebRootPath, "Images");  // Change path to "Images" folder

            // Use OutputFileName from the DTO for the merged file name
            var outputFile = $"{dto.OutputFileName}_{DateTime.Now.Ticks}.pdf";
            var outputPath = Path.Combine(rootPath, outputFile);

            using (var outputDoc = new PdfDocument())
            {
                // Loop through the uploaded files
                foreach (var file in dto.Files)
                {
                    using var stream = file.OpenReadStream();
                    var inputDoc = PdfReader.Open(stream, PdfDocumentOpenMode.Import);

                    // Add each page from the current document to the output document
                    for (int i = 0; i < inputDoc.PageCount; i++)
                        outputDoc.AddPage(inputDoc.Pages[i]);
                }
                outputDoc.Save(outputPath);
            }

            // Return the result with file details
            return new PdfUpload
            {
                FileName = dto.OutputFileName,
                FileDescription = dto.FileDescription,
                FilePath = $"/Images/{outputFile}",  // Ensure the merged file path is also in the Images folder
              
            };
        }

    }

}


