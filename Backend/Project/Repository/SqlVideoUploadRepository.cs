using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Project.Model.Domain;
using Project.Repository;

namespace matrimonybackendApi.Repositories
{
    public class SQLVideoUploadRepository : IVideoUploadRepository
    {
        private readonly IWebHostEnvironment _environment;

        public SQLVideoUploadRepository(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<VideoUpload> UploadAsync(VideoUpload video)
        {
            try
            {
                // 1. Remove extension if already added in FileName by frontend
                string baseFileName = Path.GetFileNameWithoutExtension(video.FileName);

                // 2. Sanitize file name (remove invalid chars + replace spaces)
                string sanitizedFileName = string.Concat(baseFileName.Split(Path.GetInvalidFileNameChars())).Replace(" ", "_");

                // 3. Get extension from actual uploaded file
                string fileExtension = Path.GetExtension(video.File.FileName).ToLower();

                // 4. Final file name = sanitized + actual extension
                string finalFileName = $"{sanitizedFileName}{fileExtension}";

                // 5. Folder path inside wwwroot/Images
                var videoFolder = Path.Combine(_environment.WebRootPath, "Images");

                if (!Directory.Exists(videoFolder))
                {
                    Directory.CreateDirectory(videoFolder);
                }

                // 6. Combine full local path
                var localFilePath = Path.Combine(videoFolder, finalFileName);

                // 7. Save file
                using var stream = new FileStream(localFilePath, FileMode.Create);
                await video.File.CopyToAsync(stream);

                // 8. Save file path for client access
                video.FilePath = $"/Images/{finalFileName}";

                return video;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Video Upload Failed: {ex.Message}");
                throw;
            }
        }

    }
}
