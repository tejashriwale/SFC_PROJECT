using Project.Model.Domain;

namespace Project.Repository
{
    public interface IAddUpdPRequisitionDetailsRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<AddUpdPRequisitionDetails> pRequisitionDetailsList);
    }
}
