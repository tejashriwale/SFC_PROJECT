using Project.Model.Domain;

namespace Project.Repository
{
    public interface IAddGoodReceiptNoteDetailsRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<AddGoodReceiptNoteDetails> addGoodReceipt);
    }
}
