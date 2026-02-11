using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeletePGoodReceiptNoteRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeletePGoodReceiptNote input);
    }
}
