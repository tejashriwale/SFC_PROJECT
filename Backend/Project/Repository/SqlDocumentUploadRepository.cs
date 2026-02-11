using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlDocumentUploadRepository : IDocumentUploadRepository
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbContext;

        public SqlDocumentUploadRepository(IWebHostEnvironment environment, IConfiguration configuration, ApplicationDbContext dbContext)
        {
            _environment = environment;
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public async Task<List<DocumentUpload>> UploadAsync(List<DocumentUpload> documents)
        {
            var uploadedDocs = new List<DocumentUpload>();

            var rootPath = _environment.WebRootPath ?? _configuration["UploadPath"] ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var docFolder = Path.Combine(rootPath, "Images");

            if (!Directory.Exists(docFolder))
                Directory.CreateDirectory(docFolder);

            foreach (var doc in documents)
            {
                var sanitizedFileName = Path.GetFileNameWithoutExtension(doc.FileName).Replace(" ", "_");
                string fileExt = Path.GetExtension(doc.File.FileName).ToLower();
                string finalName = $"{sanitizedFileName}{fileExt}";

                string path = Path.Combine(docFolder, finalName);
                using var stream = new FileStream(path, FileMode.Create);
                await doc.File.CopyToAsync(stream);

                doc.FilePath = $"/Images/{finalName}";
                uploadedDocs.Add(doc);
            }

            return uploadedDocs;
        }
    }
}
