using Project.Model.Domain;

namespace Project.Repository
{
    public interface IAddUpdPReturnDetailsRepository
    {
        Task<List<ApiResponseMessage>>getasync(List<AddUpdPReturnDetails> pReturnDetails);
    }
}
