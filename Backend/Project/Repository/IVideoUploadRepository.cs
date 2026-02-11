using Project.Model.Domain;

namespace Project.Repository
{
    public interface IVideoUploadRepository
    {
        Task<VideoUpload> UploadAsync(VideoUpload video);
    }
}
