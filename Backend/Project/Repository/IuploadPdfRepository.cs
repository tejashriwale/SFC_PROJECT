using matrimonybackendApi.Models.Domain;
using matrimonybackendApi.Models.DTO;
using Project.Model.Domain;

namespace Project.Repository
{
    public interface IuploadPdfRepository
    {
        Task<uploadPdf> UploadAsync(uploadPdf pdf);
    }
}
