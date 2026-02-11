using Project.Model.Domain;

namespace Project.Repository
{
    public interface IAddGoodReceiptNoteMasterRepository
    {
        Task<List<ApiResponseMessage>> getasync(AddGoodReceiptNoteMaster addGoodReceipt);
    }
}
