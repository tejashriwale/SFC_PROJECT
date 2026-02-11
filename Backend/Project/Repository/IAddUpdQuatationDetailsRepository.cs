using Project.Model.Domain;

namespace Project.Repository
{
    public interface IAddUpdQuatationDetailsRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<AddUpdQuatationDetails> quatationDetails);
    }
}
