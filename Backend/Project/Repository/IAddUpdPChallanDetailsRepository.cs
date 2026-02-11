using Project.Model.Domain;

namespace Project.Repository
{
    public interface IAddUpdPChallanDetailsRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<AddUpdPChallanDetails> pChallanDetails);
    }
}
