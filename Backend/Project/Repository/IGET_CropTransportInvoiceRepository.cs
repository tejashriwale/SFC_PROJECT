using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CropTransportInvoiceRepository
    {
        Task<List<GET_CropTransportInvoice>> getasync(GET_CropTransportInvoiceDomain input);
    }
}
