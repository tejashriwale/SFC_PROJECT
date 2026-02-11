using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdCropTransportInvoiceRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdCropTransportInvoice input);
    }
}
