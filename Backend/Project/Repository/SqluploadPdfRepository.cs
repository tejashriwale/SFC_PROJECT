using Microsoft.AspNetCore.Hosting;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqluploadPdfRepository : IuploadPdfRepository
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _dbContext;

        public SqluploadPdfRepository(IWebHostEnvironment environment, ApplicationDbContext dbContext)
        {
            _environment = environment;
            _dbContext = dbContext;
        }

        public async Task<uploadPdf> UploadAsync(uploadPdf pdf)
        {
            var rootPath = Path.Combine(_environment.WebRootPath, "files");
            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);

            var ext = Path.GetExtension(pdf.File.FileName);
            var fileName = $"{Path.GetFileNameWithoutExtension(pdf.File.FileName)}_{DateTime.Now.Ticks}{ext}";
            var fullPath = Path.Combine(rootPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await pdf.File.CopyToAsync(stream);
            }

            pdf.FilePath = $"/files/{fileName}";
            pdf.FileName = fileName;

            _dbContext.uploadPdfs.Add(pdf);
            await _dbContext.SaveChangesAsync();

            return pdf;
        }
    }
}
