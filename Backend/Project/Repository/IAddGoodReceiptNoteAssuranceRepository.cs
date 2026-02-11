using Project.Model.Domain;

namespace Project.Repository
{
    public interface IAddGoodReceiptNoteAssuranceRepository
    {
        Task<List<ApiResponseMessage>> getasync(AddGoodReceiptNoteAssurance addGoodReceipt);
    }
}
