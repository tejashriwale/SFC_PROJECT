using Project.Model.Domain;

namespace Project.Repository
{
    public interface IReturnPolicyRepository
    {
        Task<List<ApiResponseMessage>> getasync(ReturnPolicy returnPolicy);
    }
}
