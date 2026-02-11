using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteSBillRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_DeleteSBill input);
    }
}
