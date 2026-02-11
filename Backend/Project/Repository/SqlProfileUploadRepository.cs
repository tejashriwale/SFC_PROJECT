using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlProfileUploadRepository : IProfileUploadRepository
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbContext;

        public SqlProfileUploadRepository(IWebHostEnvironment environment, IConfiguration configuration, ApplicationDbContext dbContext)
        {
            _environment = environment;
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public async Task<List<ProfileUpload>> UploadAsync(List<ProfileUpload> images)
        {
            var uploadedFiles = new List<ProfileUpload>();

            try
            {
                // ✅ Common & Safe WebRootPath setup
                var rootPath = _environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

                var imagesFolder = Path.Combine(rootPath, "Images");
                if (!Directory.Exists(imagesFolder))
                {
                    Directory.CreateDirectory(imagesFolder);
                }

                foreach (var image in images)
                {
                    var sanitizedFileName = Path.GetFileNameWithoutExtension(image.FileName)
                                               .Replace(" ", "_")
                                               .Replace("/", "_")
                                               .Replace("\\", "_");

                    string fileExtension = Path.GetExtension(image.File.FileName).ToLower();
                    string finalFileName = $"{sanitizedFileName}{fileExtension}";

                    var localFilePath = Path.Combine(imagesFolder, finalFileName);
                    Console.WriteLine($"[DEBUG] Saving file to: {localFilePath}");

                    using var stream = new FileStream(localFilePath, FileMode.Create);
                    await image.File.CopyToAsync(stream);

                    image.FilePath = $"/Images/{finalFileName}";
                    uploadedFiles.Add(image);
                }

                return uploadedFiles;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] File Upload Failed: {ex.Message}");
                throw;
            }
        }
    }
}
