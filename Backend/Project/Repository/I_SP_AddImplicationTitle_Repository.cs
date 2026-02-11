using Project.Model.Domain;

namespace Project.Repository
{
    public interface I_SP_AddImplicationTitle_Repository
    {
        Task<List<ApiResponseMessage>> getasync(_SP_AddImplicationTitle_ sP_AddImplicationTitle_);
    }
}
