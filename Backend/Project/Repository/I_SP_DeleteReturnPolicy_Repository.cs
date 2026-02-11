using Project.Model.Domain;

namespace Project.Repository
{
    public interface I_SP_DeleteReturnPolicy_Repository
    {
        Task<List<ApiResponseMessage>>getasync(_SP_DeleteReturnPolicy_ input);
    }
}
