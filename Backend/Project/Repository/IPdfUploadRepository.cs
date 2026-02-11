using matrimonybackendApi.Models.Domain;
using matrimonybackendApi.Models.DTO;

namespace matrimonybackendApi.Repositories
{
    public interface IPdfUploadRepository
    {
        Task<PdfUpload> UploadAsync(PdfUpload pdf);
        Task<PdfUpload> MergeAsync(PdfMergeDto dto);
    }
}
