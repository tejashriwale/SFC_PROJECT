using Project.Model.Domain;

namespace Project.Repository
{
    public interface IProfileUploadRepository
    {
        Task<List<ProfileUpload>> UploadAsync(List<ProfileUpload> images);
    }
}
