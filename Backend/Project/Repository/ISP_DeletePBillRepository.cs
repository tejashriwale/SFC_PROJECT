using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeletePBillRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_DeletePBill input);
    }
}
