using Project.Model.Domain;

namespace Project.Repository
{
    public interface IDocumentUploadRepository
    {
        Task<List<DocumentUpload>> UploadAsync(List<DocumentUpload> documents);
    }
}
