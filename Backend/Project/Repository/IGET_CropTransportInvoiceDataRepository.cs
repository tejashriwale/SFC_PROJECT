using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CropTransportInvoiceDataRepository
    {
        Task<List<GET_CropTransportInvoiceData>> getasync(GET_CropTransportInvoiceDataDomain input);
    }
}
