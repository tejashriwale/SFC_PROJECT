using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_ReceiptDetailsByIdRepository
    {
        Task<List<GET_ReceiptDetailsById>>getasync(GET_ReceiptDetailsByIdDomain input);
    }
}
